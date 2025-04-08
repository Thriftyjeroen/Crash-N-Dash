using UnityEngine;
using System.Collections.Generic;

public static class ObjectPooler
{
    public static Dictionary<string, Queue<Component>> poolDictionary = new Dictionary<string, Queue<Component>>();

    // Adds an object back to the pool when it's no longer needed
    public static void EnqueueObject<T>(T item, string name) where T : Component
    {
        if (!item.gameObject.activeSelf) return;

        item.transform.position = Vector3.zero; // Reset position
        if (!poolDictionary.ContainsKey(name))
        {
            poolDictionary[name] = new Queue<Component>();
        }

        poolDictionary[name].Enqueue(item);
        item.gameObject.SetActive(false); // Deactivate the object
    }

    // Retrieves an object from the pool OR creates a new one if the pool is empty
    public static T DequeueObject<T>(string key, T prefab) where T : Component
    {
        if (!poolDictionary.ContainsKey(key) || poolDictionary[key].Count == 0)
        {
            // Pool is empty create a new object and add it to the pool
            T newInstance = Object.Instantiate(prefab);
            return newInstance;
        }

        // Retrieve an object from the pool
        return (T)poolDictionary[key].Dequeue();
    }

    // Sets up a pool by pre-creating a number of objects
    public static void SetupPool<T>(T pooledPrefab, int poolSize, string dictionaryEntry) where T : Component
    {
        if (!poolDictionary.ContainsKey(dictionaryEntry))
        {
            poolDictionary[dictionaryEntry] = new Queue<Component>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            T pooledInstance = Object.Instantiate(pooledPrefab);
            pooledInstance.gameObject.SetActive(false);
            poolDictionary[dictionaryEntry].Enqueue(pooledInstance);
        }
    }
}
