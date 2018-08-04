using System;
using System.Linq;
using System.Reflection;
using DDDExample.SharedKernel.Cqrs.Command;
using DDDExample.SharedKernel.Cqrs.Query;
using DDDExample.SharedKernel.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DDDExample.SharedKernel.Cqrs.Extentions
{
    public static class CqrsExtentions
    {
        public static void AddCqrs<T>(this IServiceCollection services, Func<AssemblyName, bool> filter = null)
        {
            var handlers = new[] { typeof(IQueryHandler<,>), typeof(ICommandHandler<>) };

            var types = from t in typeof(T).GetTypesAssembly(filter)
                        from i in t.GetInterfaces()
                        where i.IsConstructedGenericType &&
                              handlers.Contains(i.GetGenericTypeDefinition())
                        select new { i, t };

            foreach (var tp in types) services.AddScoped(tp.i, tp.t);
        }
    }
}
