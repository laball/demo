using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lee.Abp.EntityFrameworkCore
{
    public class BoolToIntConverter : ValueConverter<bool,int>
    {
        public BoolToIntConverter([CanBeNull] ConverterMappingHints mappingHints = null)
            : base(
                  v => Convert.ToInt32(v),
                  v => Convert.ToBoolean(v),
                  mappingHints)
        {
        }

        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(int), typeof(bool), i => new BoolToIntConverter(i.MappingHints));
    }
}
