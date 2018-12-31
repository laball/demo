using System.Diagnostics;

namespace AlgorithmCoreDemo
{
    /// <summary>
    /// 链表相关
    /// </summary>
    public static class LinkedListRelated
    {
        /// <summary>
        /// https://leetcode-cn.com/problems/add-two-numbers/description/
        /// 
        /// 输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
        /// 输出：7 -> 0 -> 8
        /// 原因：342 + 465 = 807
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode<int> AddTwoNumbers(ListNode<int> l1, ListNode<int> l2)
        {
            var node1 = l1;
            var node2 = l2;

            var node = new ListNode<int>(0);
            var head = node;
            var temp = 0;

            while (node1 != null || node2 != null || temp > 0)
            {
                var sum = (node1 != null ? node1.val : 0) + (node2 != null ? node2.val : 0);
                node.val = (sum + temp) % 10 ;
                temp = (sum + temp) / 10;

                node1 = node1 != null ? node1.next : null;
                node2 = node2 != null ? node2.next : null;

                if (temp > 0 || node1 != null || node2 != null)
                {
                    node.next = new ListNode<int>(0);
                    node = node.next;
                }
            }

            return head;
        }
    }


    public class ListNode<T>
    {
        public T val;
        public ListNode<T> next;
        public ListNode(T x) { val = x; }

        public ListNode<T> Print()
        {
            var node = this;
            var temp = string.Empty;

            while (node != null)
            {
                temp += node.val;
                node = node.next;
            }

            Trace.WriteLine(temp);

            return this;
        }

    }
}
