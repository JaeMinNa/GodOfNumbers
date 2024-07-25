using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }

    public void Init()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public void Release()
    {

    }

    public GameObject CreateBlock(string poolName, Vector3 startPosition)
    {
        GameObject block = ObjectPool.SpawnFromPool(poolName);
        block.transform.position = startPosition;
        block.SetActive(true);
        return block;
    }
}