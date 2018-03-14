using System;
using System.Collections.Generic;

namespace fonedynamics
{

    public class dynamics
    {
        private static CustomCache<int, string> _cache;

        static void Main()
        {
            _cache = new CustomCache<int, string>(3);

            lock(_cache)
            {
                _cache.AddOrUpdate(1, "test1");
                _cache.AddOrUpdate(2, "test2");
                _cache.AddOrUpdate(3, "test3");
                _cache.AddOrUpdate(4, "test4");
                _cache.AddOrUpdate(5, "test5");

                string strValue;
                //Console.Write(_cache.Remove(5));
                Console.Write(_cache.TryGetValue(5, out strValue));
                Console.ReadLine();
            }
           
        }
    }
    public class CustomCache<TKey, TValue> : ICache<TKey, TValue>
    {
        Dictionary<TKey, TValue> dict;
        Queue<TKey> queue;
        int size;        

        public CustomCache(int size)
        {
            this.size = size;
            dict = new Dictionary<TKey, TValue>(size + 1);
            queue = new Queue<TKey>(size);
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
                if (queue.Count == size)
                    dict.Remove(queue.Dequeue());
                queue.Enqueue(key);
            }
        }

        public bool Remove(TKey key)
        {
            if (dict.Remove(key))
            {
                Queue<TKey> newQueue = new Queue<TKey>(size);
                foreach (TKey item in queue)
                    if (!dict.Comparer.Equals(item, key))
                        newQueue.Enqueue(item);
                queue = newQueue;
                return true;
            }
            else
                return false;
        }  

        public bool TryGetValue(TKey key, out TValue value)
        {
            bool result = false;
            TValue outValue = default(TValue);
            if (dict.ContainsKey(key))
            {
                outValue = dict[key];
                result = true;
            }
            value = outValue;
            return result;            
        }
    }
}
