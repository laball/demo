using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreDemo
{
    public static class StringRelated
    {
        private const byte Mix = (byte)'a' - (byte)'A';

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLowerCase(string str)
        {
            var array = str.ToCharArray();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= 'A' && array[i] <= 'Z')
                {
                    array[i] = (char)((byte)array[i] + Mix);
                }
            }

            return new string(array);
        }

        /// <summary>
        /// 3. 无重复字符的最长子串
        /// 
        /// https://leetcode-cn.com/problems/longest-substring-without-repeating-characters/description/
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string str)
        {
            var hash = new Hashtable();
            var maxLength = 0;
            var minIndex = 0;

            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];

                if (hash.ContainsKey(ch))
                {
                    maxLength = Math.Max(hash.Count, maxLength);

                    var index = (int)hash[ch];

                    for (int j = minIndex; j <= index; j++)
                    {
                        if (hash.ContainsKey(str[j]) && (int)hash[str[j]] <= index)
                        {
                            hash.Remove(str[j]);
                            minIndex++;
                        }
                    }
                }

                hash[ch] = i;
            }

            return Math.Max(hash.Count, maxLength);
        }


    }
}
