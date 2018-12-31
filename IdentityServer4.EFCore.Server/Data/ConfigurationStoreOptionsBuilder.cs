﻿using IdentityServer4.EntityFramework.Options;

namespace IdentityServer4.EFCore.Server.Data
{
    public static class ConfigurationStoreOptionsBuilder
    {
        public static ConfigurationStoreOptions Build()
        {
            var options = new ConfigurationStoreOptions
            {
                //此处，可以IdentityServer4所使用的表名称进行自定义设置(TableConfiguration类型属性)；
            };

            return options;
        }
    }
}
