using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public int poolID;
    public string poolName;
    public GameObject objectPrefab;
    public int suggestedPoolSize;
    public Stack<GameObject> objectsFree = new Stack<GameObject>();
    public List<GameObject> objectsInUse = new List<GameObject>();
}



