using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class ObjectPoolMain : MonoBehaviour
{
    [SerializeField] GameObjectBoolSOEvent despawnEvent;
    [SerializeField] List<Pool> objectPools;
    [SerializeField] public int maxPoolFillRate = 50;

    public bool running = true;


    private void Awake() 
    { 
        AssignPoolIDs();
        PopulatePoolsOnAwake();
        despawnEvent.AddListener(ReturnObject);
    }

    private void AssignPoolIDs()
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            objectPools[i].poolID = i;
        }
    }

    private void Start() 
    {
        StartCoroutine(PopulatePools());
    }

    // Used by external methods to get an object from one of the pools under this class's management.
    public GameObject GetObject(string poolName)
    {
        foreach (var pool in objectPools)
        {
            if (pool.poolName == poolName) return GetObject(pool);
        }

        throw new System.Exception(string.Format("CUSTOM ERROR: Invalid pool name provided (Could not find Pool with poolName = {0})", poolName));
    }

    // Used by external methods to get an object from one of the pools under this class's management. 
    public GameObject GetObject(int poolID)
    {

    foreach (var pool in objectPools)
        {
            if (pool.poolID == poolID) return GetObject(pool);
        }

        throw new System.Exception(string.Format("CUSTOM ERROR: Invalid pool ID provided (Could not find Pool with poolID = {0})", poolID));
    }

    // Returns the number of Objects in use for a pool with provied poolName. Returns -1 if cannot find pool with provided poolName.
    public int GetNumberOfObjectsInUse(string poolName)
    {
        foreach (var pool in objectPools)
        {
            if (pool.poolName == poolName) return pool.objectsInUse.Count;
        }
        return -1;
    }

    // Returns the number of Objects in use for a pool with provied poolName. Returns -1 if cannot find pool with provided poolName.
    public int GetNumberOfObjectsInUse(int poolID)
    {
        foreach (var pool in objectPools)
        {
            if (pool.poolID == poolID) return pool.objectsInUse.Count;
        }
        return -1;
    }

    // Used by external methods to return objects to one of the pools under this class's management.
    public void ReturnObject(GameObject objectToReturn, bool dropFlag)
    {
        int poolID = objectToReturn.GetComponent<ObjectIdentifier>().poolID;
        foreach (var pool in objectPools)
        {
            if (pool.poolID == poolID) 
            {
                ReturnObject(objectToReturn, pool);
                return;
            }
        }

        throw new System.Exception(string.Format("CUSTOM ERROR: Invalid pool ID provided by parameter GameObject (Could not find Pool with poolID = {0})",
         poolID));
    }


    public int calledXTimes = 0;

    // Used internally by ObjectPoolMain to Get an object from a specified Pool.
    GameObject GetObject(Pool pool)
    {
        GameObject objectToGet;
        if (pool.objectsFree.Count == 0) 
        {
            calledXTimes ++;
            objectToGet = pool.objectsInUse[0];
            objectToGet.SetActive(true);
            pool.objectsInUse.RemoveAt(0);
            pool.objectsInUse.Add(objectToGet);
            return objectToGet;
        }

        objectToGet = pool.objectsFree.Pop();
        objectToGet.SetActive(true);
        pool.objectsInUse.Add(objectToGet);
        return objectToGet;
    }

    // Used internally by ObjectPoolMain to return an object to its pool. This is so inefficient.
    void ReturnObject(GameObject objectToReturn, Pool pool)
    {
        foreach (var item in pool.objectsInUse)
        {
            if (item.GetInstanceID() == objectToReturn.GetInstanceID()) 
            {
                // objectToReturn.transform.position = new Vector3(0, 0, 0);
                pool.objectsFree.Push(objectToReturn);
                pool.objectsInUse.Remove(objectToReturn);
                objectToReturn.SetActive(false);
                return;
            }
        }
    }



    // Call on awake to ensure there are always some objects in the pool if some other objects requests one.
    void PopulatePoolsOnAwake(){
        for (int i = 0; i < objectPools.Count; i++)
        {
            for (int j = 0; j < Mathf.Max(10, Mathf.Min((int)(500 / objectPools.Count), (int)(objectPools[i].suggestedPoolSize / 50))); j++)
            {
                CreatePoolObject(objectPools[i]);
            }
        }
    }


    IEnumerator PopulatePools()
    {
        // Initial setup of variables used in method.
        List<float> poolFillPercentages = new List<float>();
        foreach (var pool in objectPools)
        {
            poolFillPercentages.Add(0);
        }

        float minFill;
        int minFillIndex = 0;
        float tempFill = 0;

        GameObject currentObject;

        // Constantly running while Scene is active.
        while (running)
        {
            minFill = 1;
            // Find the pool index which is the lowest % full.
            for (int i = 0; i < objectPools.Count; i++)
            {
                tempFill = (float)(objectPools[i].objectsFree.Count + objectPools[i].objectsInUse.Count) / (float)objectPools[i].suggestedPoolSize;
                if (tempFill < minFill)
                {
                    minFillIndex = i;
                    minFill = tempFill;
                }
            }

            // Instantiate more objects if pool is not full.
            if (minFill < 1) 
            {
                for (int i = 0; i < Mathf.Max(1, (int) maxPoolFillRate / 50.0f); i++)
                {
                    currentObject = CreatePoolObject(objectPools[minFillIndex]);
                }
            }

            yield return new WaitForSeconds(Mathf.Max(1 / maxPoolFillRate, 0.02f));
        }

        yield return null;
    }



    // Instantiates an object pools prefab, disables it, and then tells it that it exists.
    GameObject CreatePoolObject(Pool pool)
    {
        GameObject newObject = Instantiate(pool.objectPrefab, transform);
        if (newObject.TryGetComponent(out ObjectIdentifier identity)) 
        {
            identity.poolID = pool.poolID;
            identity.poolName = pool.poolName;
        }
        else 
        {
            newObject.AddComponent<ObjectIdentifier>();
            newObject.GetComponent<ObjectIdentifier>().poolName = pool.poolName;
            newObject.GetComponent<ObjectIdentifier>().poolID = pool.poolID;
        }
        newObject.gameObject.SetActive(false);
        pool.objectsFree.Push(newObject);
        return newObject;
    }

}
