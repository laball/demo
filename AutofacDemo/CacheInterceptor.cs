using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Castle.DynamicProxy;

namespace AutofacDemo
{
    public class CacheInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var attr = invocation.GetConcreteMethod().GetCustomAttributes(typeof(CacheAttribute),false);
            var cacheAttr = attr[0] as CacheAttribute;
            var key = cacheAttr.Key;

            //get data from cache

            invocation.ReturnValue = 10;

            //if not call the invocation.Proceed() the function will not really exec
        }
    }

    public class InterceptorModule : Autofac.Module
    {
        // This is a private constant from the Autofac.Extras.DynamicProxy2 assembly
        // that is needed to "poke" interceptors into registrations.
        const string InterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName";

        protected override void Load(ContainerBuilder builder)
        {
            // Register global interceptors here.
            builder.Register(c => new CacheInterceptor());
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Here is where you define your "global interceptor list"
            var interceptorServices = new Service[] { new TypedService(typeof(CacheInterceptor)) };

            // Append the global interceptors to any existing list, or create a new interceptor
            // list if none are specified. Note this info will only be used by registrations
            // that are set to have interceptors enabled. It'll be ignored by others.
            object existing;
            if (registration.Metadata.TryGetValue(InterceptorsPropertyName, out existing))
            {
                registration.Metadata[InterceptorsPropertyName] =
                  ((IEnumerable<Service>)existing).Concat(interceptorServices).Distinct();
            }
            else
            {
                registration.Metadata.Add(InterceptorsPropertyName, interceptorServices);
            }
        }
    }

}
