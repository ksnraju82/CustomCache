using System;
using System.Collections.Generic;

namespace CacheLibrary
{
    public class CustomCache<TKey, TValue> : ICache<TKey, TValue>
    {
        Dictionary<TKey, TValue> dict;
        List<TKey> list;
        int size;

        public CustomCache(int size)
        {
            this.size = size;
            dict = new Dictionary<TKey, TValue>(size);
            list = new List<TKey>();
        }

        //The logic to capture the least recently added/updated was to,
        //remove the added/updated item from the list and add it back again so that it will
        //take the last place in the list and list[list.count - 1] will act as a key to remove it from dict.
        public void AddOrUpdate(TKey key, TValue value)
        {
            lock (dict)
            {
                lock (list)
                {
                    if (dict.ContainsKey(key))
                    {
                        dict[key] = value;
                        list.Remove(key);
                    }
                    else
                    {
                        dict.Add(key, value);
                        if (list.Count == size)
                        {
                            dict.Remove(list[list.Count - 1]);
                            list.Remove(list[list.Count - 1]);
                        }
                    }
                    list.Add(key);
                }
            }
        }

        //The logic to capture the least recently Retrieved was to,
        //remove the Retrieved item from the list and add it back again so that it will
        //take the last place in the list and list[list.count - 1] will act as a key to remove it from dict.
        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                lock (dict)
                {
                    bool result = false;
                    //setting the value to default, in case the key does not exist in dictionary. 
                    TValue outValue = default(TValue);

                    if (dict.ContainsKey(key))
                    {
                        outValue = dict[key];
                        result = true;

                        list.Remove(key);                        
                        list.Add(key);
                    }
                    value = outValue;
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
