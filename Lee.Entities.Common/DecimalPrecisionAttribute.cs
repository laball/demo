using System;

namespace Lee.Entities.Common
{
    /// <summary>
    /// 自定义Decimal类型的精度属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DecimalPrecisionAttribute : Attribute
    {
        /// <summary>
        /// 自定义Decimal类型的精确度属性
        /// </summary>
        /// <param name="precision">精度（默认18）</param>
        /// <param name="scale">小数位数（默认2）</param>
        public DecimalPrecisionAttribute(byte precision = 18, byte scale = 2)
        {
            Precision = precision;
            Scale = scale;
        }
        public byte Precision { get; set; }

        public byte Scale { get; set; }
    }
}
