using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public GameObject CreateBlock(string poolName, Vector3 startPosition)
    {
        GameObject block = ObjectPool.SpawnFromPool(poolName);
        block.transform.position = startPosition;
        block.SetActive(true);
        return block;
    }
}