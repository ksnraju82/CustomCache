# CustomCache

Specifications:

The cache IS implemented using ICache<TKey, TValue> interface.
public interface ICache<TKey, TValue>
{
    /// <summary>
    /// Adds the value to the cache against the specified key.
    /// If the key already exists, its value is updated.
    /// </summary>
    void AddOrUpdate(TKey key, TValue value);
    /// <summary>
    /// Attempts to gets the value from the cache against the specified key
    /// and returns true if the key existed in the cache.
    /// </summary>
    bool TryGetValue(TKey key, out TValue value);
}

The cache was implemented an per eviction policy (below).

When the cache is constructed, it should take as an argument the maximum number of elements stored in the cache.

>> The cache size is taken while initializing the customcache class.

When an item is added to the cache, a check should be run to see if the cache size exceeds the maximum number of elements permitted. If this is the case, then the least recently added/updated/retrieved item should be evicted from the cache.

>> a c# dictiionary and queue are used to satisfy the condition "least recently" needs to deleted in case of max cache size is reached

The cache must be unit tested.

>> All the cases are unit tested

All operations, including cache eviction, must have O(1) time complexity.

>> All the conditions are O(1)  compliant

The cache must be thread-safe. Your consumers will be using the cache from a variety of threads simultaneously.

>> The class is locked to maintain thread-safe implementation
