1  程序集“Abp.Web.Api, Version=3.7.2.0, Culture=neutral, PublicKeyToken=null”
中的类型“Abp.WebApi.Validation.AbpApiValidationFilter”的方法“ExecuteActionFilterAsync”没有实现。

巨坑；
参见：http://www.cnblogs.com/w2011/p/7871868.html
System.Net.Http的版本需要设置为“4.2.0.0”

Using a Hub instance not created by the HubPipeline is unsupported.

Abp 中默认是没有开启多租户的，并且TenantId默认为1；

页面在登录后，将当前用户信息写入到Cookie，如此，Signalr连接时，会将Cookie带到服务端
即可将ConnectionId和用户信息进行绑定；












