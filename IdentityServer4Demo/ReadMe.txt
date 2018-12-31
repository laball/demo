

参考：https://www.cnblogs.com/liumengchen-boke/p/8337065.html


问题与发现：
1、 发现IdentityServer4与ABP直接集成时（ABP有动态API生成），发现，通过Swagger或PostMan测试接口是报404，
拆分后，去除动态API生成部分后可用，也就是将带有ABP动态API的启动程序与IdentityServer4授权部分拆分，可用；

2、Abp.ZeroCore模块比较复杂，后期需要花点时间研究研究，深入探索；