using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> where T : MonoBehaviour, IPoolable
{
    private Stack<T> pooledObjects;
    private T loadedObject;

    public ObjectPooling(string path, int initialSize)
    {
        pooledObjects = new Stack<T>(initialSize);
        loadedObject = Resources.Load<T>(path);

        for (int i = 0; i < initialSize; i++)
        {
            T obj = InstantiateObject();
            pooledObjects.Push(obj);
        }
    }

    public T Rent()
    {
        T obj;

        if (!pooledObjects.TryPop(out obj))
        {
            obj = InstantiateObject();
        }

        obj.SetDisableCallbackAction(this.TurnBack);

        return obj;
    }

    public void TurnBack(IPoolable obj)
    {
        T TObj = (T)obj;
        TObj.gameObject.SetActive(false);
        pooledObjects.Push(TObj);
    }

    private T InstantiateObject()
    {
        T obj = Object.Instantiate(loadedObject);
        obj.gameObject.SetActive(false);
        return obj;
    }
}