using System.Collections;
using System.Collections.Generic;

namespace AlgorithmCoreDemo
{
    /// <summary>
    /// https://leetcode-cn.com/problems/lru-cache/description/
    /// 146. LRU缓存机制
    /// 
    /// 示例：
    /// LRUCache cache = new LRUCache( 2 /* 缓存容量 */ );
    /// cache.put(1, 1);
    /// cache.put(2, 2);
    /// cache.get(1);       // 返回  1
    /// cache.put(3, 3);    // 该操作会使得密钥 2 作废
    /// cache.get(2);       // 返回 -1 (未找到)
    /// cache.put(4, 4);    // 该操作会使得密钥 1 作废
    /// cache.get(1);       // 返回 -1 (未找到)
    /// cache.get(3);       // 返回  3
    /// cache.get(4);       // 返回  4
    /// 
    /// 设计思路：
    ///     使用Hash和双链表实现，复杂度为O(1)，空间换时间；
    /// 
    /// </summary>
    public class LRUCache
    {
        private readonly int _capacity;

        private DoubleLinkedListNode<KeyValuePair<int, int>> head;

        private DoubleLinkedListNode<KeyValuePair<int, int>> tail;

        private Hashtable hashtable = new Hashtable();

        public LRUCache(int capacity)
        {
            _capacity = capacity;
        }

        public int Get(int key)
        {
            try
            {
                if (!hashtable.ContainsKey(key))
                {
                    return -1;
                }

                var findNode = (DoubleLinkedListNode<KeyValuePair<int, int>>)hashtable[key];
                if (findNode == head)
                {
                    return head.Data.Value;
                }

                if (findNode == tail)
                {
                    var tailPrior = tail.Prior;
                    tailPrior.Next = null;

                    tail.Prior = null;
                    tail.Next = head;

                    head.Prior = tail;

                    head = tail;
                    tail = tailPrior;

                    return head.Data.Value;
                }

                var findNodeNext = findNode.Next;
                var findNodeProir = findNode.Prior;

                findNodeProir.Next = findNodeNext;
                findNodeNext.Prior = findNodeProir;

                findNode.Prior = null;
                findNode.Next = head;

                head.Prior = findNode;

                head = findNode;

                return head.Data.Value;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(Print(), ex);
            }
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Put(int key, int value)
        {
            try
            {
                if (head == null)
                {
                    var node = new DoubleLinkedListNode<KeyValuePair<int, int>>()
                    {
                        Data = new KeyValuePair<int, int>(key, value),
                        Prior = null,
                        Next = null
                    };

                    hashtable[key] = tail = head = node;

                    return;
                }

                if (hashtable.ContainsKey(key))
                {
                    var findNode = (DoubleLinkedListNode<KeyValuePair<int, int>>)hashtable[key];

                    if (findNode == head)
                    {
                        head.Data = new KeyValuePair<int, int>(key, value);
                        return;
                    }

                    if (findNode == tail)
                    {
                        tail.Data = new KeyValuePair<int, int>(key, value);

                        var tailPrior = tail.Prior;
                        tailPrior.Next = null;

                        tail.Prior = null;
                        tail.Next = head;

                        head.Prior = tail;

                        head = tail;

                        tail = tailPrior;

                        return;
                    }

                    hashtable.Remove(findNode.Data.Key);

                    var findNodeNext = findNode.Next;
                    var findNodeProir = findNode.Prior;

                    findNodeProir.Next = findNodeNext;
                    findNodeNext.Prior = findNodeProir;

                    findNode.Prior = findNode.Next = null;

                    var newNode = new DoubleLinkedListNode<KeyValuePair<int, int>>()
                    {
                        Data = new KeyValuePair<int, int>(key, value),
                        Prior = null,
                        Next = head
                    };

                    hashtable[key] = newNode;

                    head.Prior = newNode;

                    head = newNode;

                    return;
                }

                if (hashtable.Count == _capacity)
                {
                    if (head != tail)
                    {
                        hashtable.Remove(tail.Data.Key);

                        var temp = tail.Prior;
                        if (temp != null)
                        {
                            temp.Next = null;
                        }

                        tail.Prior = null;
                        tail = temp;

                        var newNode = new DoubleLinkedListNode<KeyValuePair<int, int>>()
                        {
                            Data = new KeyValuePair<int, int>(key, value),
                            Prior = null,
                            Next = head
                        };

                        hashtable[key] = newNode;

                        head.Prior = newNode;
                        head = newNode;
                    }
                    else
                    {
                        if (head.Data.Key == key)
                        {
                            head.Data = new KeyValuePair<int, int>(key, value);
                        }
                        else
                        {
                            //key不同时，需要移除Hash表中的节点

                            hashtable.Remove(tail.Data.Key);

                            var newNode = new DoubleLinkedListNode<KeyValuePair<int, int>>()
                            {
                                Data = new KeyValuePair<int, int>(key, value),
                                Prior = null,
                                Next = head
                            };

                            hashtable[key] = tail = head = newNode;
                        }
                    }
                }
                else
                {
                    AddToHead(key, value);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(Print(), ex);
            }
        }

        void AddToHead(int key, int value)
        {
            var newNode = new DoubleLinkedListNode<KeyValuePair<int, int>>()
            {
                Data = new KeyValuePair<int, int>(key, value),
                Prior = null,
                Next = head
            };

            hashtable[key] = newNode;

            head.Prior = newNode;

            head = newNode;
        }


        public string Print()
        {
            var nodeStr = string.Empty;

            var node = head;

            while (node != null)
            {
                nodeStr += node.Data.ToString();

                node = node.Next;
            }

            return $"capacity: {_capacity} , Head:{head?.Data}, Tail:{tail?.Data},nodes: {nodeStr}";
        }

        internal class DoubleLinkedListNode<T>
        {
            public T Data { get; set; }
            public DoubleLinkedListNode<T> Next { get; set; }
            public DoubleLinkedListNode<T> Prior { get; set; }
        }

        internal class DoubleLinkedList<T>
        {
            public DoubleLinkedListNode<T> Head { get; }

            public DoubleLinkedListNode<T> Tail { get; }



        }

    }

}
