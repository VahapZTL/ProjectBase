using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation?.Method?.ReflectedType?.FullName}.{invocation?.Method.Name}");
            var arguments = invocation?.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";

            if (_cacheManager.IsAdd(key))
            {
                //var cachedData = _cacheManager.Get(key);

                //if (cachedData is IDataResult<IEnumerable<object>> list && invocation?.Arguments.Length > 0)
                //{
                //    var filteredData = ApplyLinqFilters(list.Data, invocation.Arguments);
                //    invocation.ReturnValue = filteredData;
                //    return;
                //}

                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            invocation.Proceed();

            _cacheManager.Add(key, invocation.ReturnValue,_duration);
        }

        private IDataResult<IEnumerable<object>> ApplyLinqFilters(IEnumerable<object> list, object[] parameters)
        {
            var queryableList = list.AsQueryable();

            foreach (var param in parameters)
            {
                if (param is Expression<Func<object, bool>> expression) // Filtreleme
                {
                    queryableList = queryableList.Where(expression);
                }
                else if (param is Func<IQueryable<object>, IQueryable<object>> linqOperation) // Diğer LINQ işlemleri
                {
                    queryableList = linqOperation(queryableList);
                }
            }

            return new DataResult<IEnumerable<object>>(queryableList.ToList(), true, "Filtered data");
        }
    }
}
