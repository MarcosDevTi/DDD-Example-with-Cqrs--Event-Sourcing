using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDDExample.SharedKernel.Extensions
{
    public static class TypeAssemblyExtentions
    {
        public static IEnumerable<Type> GetTypesAssembly(this Type type, Func<AssemblyName, bool> filter)
        {
            var target = type.Assembly;
            bool FilterTrue(AssemblyName x) => true;

            var assemblies = target.GetReferencedAssemblies()
                .Where(filter ?? FilterTrue)
                .Select(Assembly.Load)
                .ToList();
            assemblies.Add(target);

            return assemblies.SelectMany(a => a.GetExportedTypes());
        }
    }
}
