using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool", menuName = "Object Pool")]
public class ObjectPoolSO : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> objectPool = new Queue<GameObject>();

    public void Initialize()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count == 0)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        GameObject pooledObject = objectPool.Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
