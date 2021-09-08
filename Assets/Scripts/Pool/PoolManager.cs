using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolItem
{
    public GameObject prefab;
    public int poolSize;
}

public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    [SerializeField]
    private List<PoolItem> poolItems;

    private Dictionary<int, Queue<GameObject>> pool;

    private void Awake()
    {
        base.Awake();
        pool = new Dictionary<int, Queue<GameObject>>();
        foreach (var poolItem in poolItems)
        {
            pool.Add(poolItem.prefab.GetInstanceID(), new Queue<GameObject>());
            CreatePool(poolItem);
        }
    }

    private void CreatePool(PoolItem poolItem)
    {
        var key = poolItem.prefab.GetInstanceID();
        var anchorObject = new GameObject($"{poolItem.prefab.name}Anchor");
        anchorObject.transform.SetParent(transform);
        for (var i = 0; i < poolItem.poolSize; ++i)
        {
            var childObject = Instantiate(poolItem.prefab, anchorObject.transform);
            pool[key].Enqueue(childObject);
            ResetObject(ref childObject);
            childObject.SetActive(false);
        }
    }

    public GameObject ReuseGameObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        var key = gameObject.GetInstanceID();

        GameObject reuseableObject = null;

        if (pool[key] != null)
        {
            reuseableObject = GetObjectToReuse(key);
            reuseableObject.transform.position = position;
            reuseableObject.transform.rotation = rotation;
        }

        return reuseableObject;
    }

    private GameObject GetObjectToReuse(int objectKey)
    {
        var objectFromPool = pool[objectKey].Dequeue();
        pool[objectKey].Enqueue(objectFromPool);
        if (objectFromPool.activeSelf)
            objectFromPool.SetActive(false);

        ResetObject(ref objectFromPool);

        return objectFromPool;
    }

    private void ResetObject(ref GameObject gameObject)
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
    }
}
