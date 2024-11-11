using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObjectPool
{
    public ObjectPool() {}

    [Serializable]
    public class ObjectPoolSlot
    {
        public GameObject prefab;
        public List<GameObject> objects = new List<GameObject>();

        public ObjectPoolSlot(GameObject prefab)
        {
            this.prefab = prefab;
        }
    }

    [SerializeField] private List<ObjectPoolSlot> objectPools = new List<ObjectPoolSlot>(); //this and the class shouldn't be serializable i just like how it looks in the editor lol

    public GameObject GetObject(GameObject prefab)
    {
        ObjectPoolSlot objectPool = FindObjectPool(prefab);

        if (objectPool == null)
        {
            objectPool = NewObjectPool(prefab);
        }

        return GetFreeObject(objectPool);
    }

    ObjectPoolSlot FindObjectPool(GameObject prefab)
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            if (objectPools[i].prefab == prefab)
            {
                return objectPools[i];
            }
        }

        return null;
    }

    ObjectPoolSlot NewObjectPool(GameObject prefab)
    {
        objectPools.Add(new ObjectPoolSlot(prefab));
        return objectPools[objectPools.Count - 1];
    }

    GameObject GetFreeObject(ObjectPoolSlot objectPool)
    {
        for (int i = 0; i < objectPool.objects.Count; i++)
        {
            if (objectPool.objects[i].activeSelf == false)
            {
                objectPool.objects[i].SetActive(true);
                return objectPool.objects[i];
            }
        }

        objectPool.objects.Add(GameObject.Instantiate(objectPool.prefab));
        return objectPool.objects[objectPool.objects.Count - 1];
    }

    public void ClearAll()
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            for (int x = 0; x < objectPools[i].objects.Count; x++)
            {
                GameObject.Destroy(objectPools[i].objects[x]);
            }
        }
    }
}