using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using static log4net.Appender.FileAppender;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly HashSet<string> _cacheKeys; 
        private readonly object _lock = new object();
        private static readonly ConcurrentDictionary<string, Lazy<Regex>> _regexCache = new();

        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>()
                     ?? throw new InvalidOperationException("IMemoryCache service is not available.");

            _cacheKeys = new HashSet<string>();
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            return _cache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(duration))
                .RegisterPostEvictionCallback((k, v, reason, state) =>
                {
                    lock (_lock)
                    {
                        _cacheKeys.Remove(k.ToString());
                    }
                });

            lock (_lock)
            {
                _cache.Set(key, data, cacheEntryOptions);
                _cacheKeys.Add(key);
            }
        }

        public bool IsAdd(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            lock (_lock)
            {
                _cache.Remove(key);
                _cacheKeys.Remove(key);
            }
        }

        public void RemoveByPattern(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Pattern cannot be null or empty.", nameof(pattern));

            try
            {
                // Regex nesnesini önbelleğe al (Lazy ile regex sadece ihtiyaç duyulduğunda derlenecek)
                var regex = _regexCache.GetOrAdd(pattern, p => new Lazy<Regex>(() => new Regex(p, RegexOptions.Compiled | RegexOptions.IgnoreCase))).Value;

                // Büyük veri kümeleri için paralel işlem
                IEnumerable<string> keysToRemove = _cacheKeys.Count > 1000
                    ? _cacheKeys.AsParallel().Where(key => regex.IsMatch(key)).ToList()
                    : _cacheKeys.Where(key => regex.IsMatch(key)).ToList();

                // Thread-safe işlem
                lock (_lock)
                {
                    foreach (var key in keysToRemove)
                    {
                        _cache.Remove(key);
                        _cacheKeys.Remove(key);
                    }
                }

                // Regex Cache'in aşırı büyümesini önlemek için temizleme işlemi
                if (_regexCache.Count > 1000)
                {
                    foreach (var oldKey in _regexCache.Keys.Take(500)) // Eski 500 deseni sil
                    {
                        _regexCache.TryRemove(oldKey, out _);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Invalid regex pattern.", nameof(pattern), ex);
            }
        }
    }
}
