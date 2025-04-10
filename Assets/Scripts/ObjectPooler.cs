using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
    public static Dictionary<string, Queue<GameObject>> poolDictionary = new();

    // Adds an object back to the pool when it's no longer needed
    public static void EnqueueObject(GameObject item, string name)
    {
        if (!item.activeSelf) return;

        item.transform.position = Vector3.zero;
        item.transform.rotation = Quaternion.identity;

        if (!poolDictionary.ContainsKey(name))
            poolDictionary[name] = new Queue<GameObject>();

        poolDictionary[name].Enqueue(item);
        item.SetActive(false);
    }

    // Retrieves an object from the pool or creates a new one if the pool is empty
    public static GameObject DequeueObject(string key, GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(key) || poolDictionary[key].Count == 0)
            return Object.Instantiate(prefab);

        return poolDictionary[key].Dequeue();
    }

    // Pre-creates a number of objects for the pool
    public static void SetupPool(GameObject pooledPrefab, int poolSize, string dictionaryEntry)
    {
        if (!poolDictionary.ContainsKey(dictionaryEntry))
            poolDictionary[dictionaryEntry] = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledInstance = Object.Instantiate(pooledPrefab);
            pooledInstance.SetActive(false);
            poolDictionary[dictionaryEntry].Enqueue(pooledInstance);
        }
    }
}
