using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ServiceStack.Redis;

namespace RedisDemo
{
    /// <summary>
    /// 修复ServiceStack.Redis V3 中AddRangeToList方法性能问题
    /// </summary>
    public static class RedisExtentions
    {
        private static readonly Lazy<MethodInfo> methodInfoLazy = new Lazy<MethodInfo>(() => typeof(RedisClient).GetMethod("SendExpectLong", BindingFlags.NonPublic | BindingFlags.Instance));
        private static MethodInfo MethodCach
        {
            get
            {
                return methodInfoLazy.Value;
            }
        }

        public static void AddRangeToListEx(this RedisClient redis, string listId, List<string> values)
        {
            AddRangeToListEx(redis as IRedisClient, listId, values);

            if (true)
            {

            }
        }

        public static void AddRangeToListEx(this IRedisClient redis, string listId, List<string> values)
        {
            var byteArray = values.Select(c => Encoding.UTF8.GetBytes(c)).ToArray();
            byte[][] bytes = MergeCommandWithArgs(Commands.RPush, Encoding.UTF8.GetBytes(listId), byteArray);
            MethodCach.Invoke(redis, new object[] { bytes });
        }

        private static byte[][] MergeCommandWithArgs(byte[] cmd, byte[] firstArg, params byte[][] args)
        {
            byte[][] bufferArray = new byte[2 + args.Length][];
            bufferArray[0] = cmd;
            bufferArray[1] = firstArg;
            for (int i = 0; i < args.Length; i++)
            {
                bufferArray[i + 2] = args[i];
            }
            return bufferArray;
        }
    }
}
