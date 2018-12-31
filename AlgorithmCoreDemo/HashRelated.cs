using System.Collections;

namespace AlgorithmCoreDemo
{
    public static class HashRelated
    {
        /// <summary>
        /// No.1
        /// https://leetcode-cn.com/problems/two-sum/description/
        /// Time:O(n)
        /// Space:O(n)
        /// </summary>
        /// <param name="nums">The nums.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static int[] TwoSum(int[] nums, int target)
        {
            var hash = new Hashtable();

            for (int i = 0; i < nums.Length; i++)
            {
                var temp = target - nums[i];

                if (hash.ContainsKey(temp))
                {
                    return new[] { (int)hash[temp], i };
                }
                else
                {
                    hash[nums[i]] = i;
                }
            }

            throw new System.InvalidOperationException("操作非法。");
        }

    }

}
