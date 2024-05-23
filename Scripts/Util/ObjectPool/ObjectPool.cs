using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int capacity;
        public int count;
        public int maxCapacity;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.capacity; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        Pool pool = pools.Find(x => x.tag == tag);
        if (pool.count >= pool.maxCapacity) return null;

        if (pool.capacity == pool.count)
        {
            CreateObject(pool);
        }

        GameObject obj = PoolDictionary[tag].Dequeue();

        while (true)
        {
            if (!obj.activeSelf) break;
            PoolDictionary[tag].Enqueue(obj);
            obj = PoolDictionary[tag].Dequeue();
        }
        PoolDictionary[tag].Enqueue(obj);

        obj.SetActive(true);
        pool.count++;

        return obj;
    }

    private void CreateObject(Pool pool)
    {
        for (int i = 0; i < pool.capacity * 2; i++)
        {
            GameObject obj = Instantiate(pool.prefab, transform);
            obj.SetActive(false);
            PoolDictionary[pool.tag].Enqueue(obj);
        }
        pool.capacity *= 3;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        PoolDictionary[obj.tag].Enqueue(obj);
        Pool pool = pools.Find(x => x.tag == obj.tag);
        pool.count--;
    }

}
