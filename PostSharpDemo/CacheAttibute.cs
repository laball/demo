﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace PostSharpDemo
{
    [Serializable]
    public sealed class CacheAttribute : MethodInterceptionAspect
    {
        private readonly string key;

        public CacheAttribute(string key)
        {
            this.key = key;
        }

        public override void OnInvoke(MethodInterceptionArgs context)
        {
            //object value;

            //if (!CacheHelper.Get(key, out value))
            //{
            //    // Do lookup based on caller's logic.
            //    value = context.Delegate.DynamicInvoke();
            //    CacheHelper.Add(value, key);
            //}

            //context.ReturnValue = value;
        }
    }
}