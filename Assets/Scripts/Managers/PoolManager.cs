using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    public ObjectPooler[] poolPlayerShots;

    [SerializeField]
    public ObjectPooler[] poolEnemyShots;

    [SerializeField]
    public ObjectPooler[] poolEnemies;

    [SerializeField]
    public ObjectPooler[] poolAsteroids;

    //Singleton class
    public static PoolManager Instance { get; private set; }

    public GameObject GrabFromPool(ObjectPooler pool)
    {
        return pool.GrabAvailableObject();
    }

    void Awake()
    {
        //Initiate Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this.gameObject);
    }
    
}
