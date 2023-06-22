using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Core.Extensions
{
    public static class SessionExtensions
    {
        public static T Get<T>(this ISession session, string key)
        {
            byte[] valueBytes = session.TryGetValue(key, out byte[] bytes) ? bytes : null;
            if (valueBytes == null)
                return default(T);

            string valueString = Encoding.UTF8.GetString(valueBytes);
            return JsonConvert.DeserializeObject<T>(valueString);
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            byte[] valueBytes = Encoding.UTF8.GetBytes(valueString);
            session.Set(key, valueBytes);
        }
    }
}
