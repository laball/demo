using System;

namespace Lee.Entities.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DefaultValueAttribute : Attribute
    {
        public object DefaultValue { get; set; }

        public string DefaultValueSql { get; set; }

        public DefaultValueAttribute() { }

        public DefaultValueAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
    }
}
