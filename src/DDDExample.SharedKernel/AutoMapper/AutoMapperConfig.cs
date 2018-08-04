using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DDDExample.SharedKernel.Extensions;

namespace DDDExample.SharedKernel.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void Register<T>(Func<AssemblyName, bool> filter = null)
        {
            var types = typeof(T).GetTypesAssembly(filter).ToList();
            Mapper.Initialize(c =>
            {
                CustomMaps(c, types);
                MapFrom(c, types);
                MapTo(c, types);
            });
        }

        public static void MapTo<TFrom, TTo>(this IMapperConfigurationExpression cfg) =>
            cfg.CreateMap(typeof(TFrom), typeof(TTo));

        public static void MapFrom<TTo, TFrom>(this IMapperConfigurationExpression cfg) =>
            cfg.CreateMap(typeof(TFrom), typeof(TTo));

        private static void MapTo(IMapperConfigurationExpression cfg, IEnumerable<Type> types)
        {
            var maps = Map(cfg, types, typeof(IMapTo<>));
            foreach (var it in maps) cfg.CreateMap(it.Item1, it.Item2);
        }

        private static void MapFrom(IMapperConfigurationExpression cfg, IEnumerable<Type> types)
        {
            var maps = Map(cfg, types, typeof(IMapFrom<>));
            foreach (var it in maps) cfg.CreateMap(it.Item2, it.Item1);
        }

        private static IEnumerable<Tuple<Type, Type>> Map(IMapperConfigurationExpression cfg, IEnumerable<Type> types, Type type) =>
                from t in types.ToList()
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == type &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select new Tuple<Type, Type>(t, i.GetGenericArguments()[0]);

        private static void CustomMaps(IMapperConfigurationExpression cfg, IEnumerable<Type> types) =>
            (from t in types.ToList()
             from i in t.GetInterfaces()
             where typeof(IMapperConfig).IsAssignableFrom(t) &&
                   !t.IsAbstract &&
                   !t.IsInterface
             select (IMapperConfig)Activator.CreateInstance(t)).ToList()
                .ForEach(m => m.Map(cfg));

    }
}
