using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    GameObject poolType;
    List<GameObject> pool;

    public GameObject GrabAvailableObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        //Did not find any available objects so creates a new one
        return CreateChild();
    }
    public GameObject CreateChild()
    {
        GameObject child = GameObject.Instantiate(poolType);
        pool.Add(child);
        child.transform.SetParent(this.gameObject.transform);
        return child;
    }
    public bool CheckIfAllDisabled()
    {
        bool toReturn = true;
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy)
            {
                toReturn = false;
            }
        }
        return toReturn;
    }
    void Awake()
    {
        pool = new List<GameObject>();
        
        GameObject child = CreateChild();
        child.SetActive(false);
    }

}
