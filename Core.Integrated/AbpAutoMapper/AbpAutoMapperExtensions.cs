using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.AutoMapper;
using AutoMapper;

namespace Core.Integrated    /// <summary>

{
    /// 使用ABP针对AutoMapper扩展，简化对象映射，
    /// ABP中AutoMapper的实现机制，通过反射，获取特定标注的类型，“自动”执行映射，
    /// 并且不开启AssertConfigurationIsValid检测，也就是默认按照名称相同的字段进行映射
    /// 没有映射到的字段不会有任何赋值操作；
    /// see:https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp.AutoMapper/AutoMapper/AbpAutoMapperModule.cs
    /// </summary>
    public static class AbpAutoMapperExtensions
    {

        public static void AbpAutoMap(this IMapperConfigurationExpression configuration, params Assembly[] assemblies)
        {
            var types = from assembly in assemblies.Where(c => c != null)
                        from type in assembly.ExportedTypes
                        where type.IsDefined(typeof(AutoMapAttribute)) ||
                            type.IsDefined(typeof(AutoMapFromAttribute)) ||
                            type.IsDefined(typeof(AutoMapToAttribute))
                        select type;

            configuration.AbpAutoMap(types);
        }

        private static void AbpAutoMap(this IMapperConfigurationExpression configuration, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                configuration.CreateAutoAttributeMaps(type);
            }
        }

        private static void CreateAutoAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
            {
                autoMapAttribute.CreateMap(configuration, type);
            }
        }
    }
}
