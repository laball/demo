using System.Collections.Generic;
using AutoMapper;

namespace Core.Extentions
{
    /// <summary>
    /// 将object to object 封装一次，降低代码侵入性
    /// </summary>
    public static class MapperUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource,TDestination>(source);
        }

        public static List<T> MapToList<T>(this object source)
        {
            return source.Map<List<T>>();
        }
    }
}
