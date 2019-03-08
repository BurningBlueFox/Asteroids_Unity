using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int wave;
    [SerializeField]
    private int baseEnemy1 = 1;
    [SerializeField]
    private int incrementEnemy1 = 2;
    [SerializeField]
    private int baseEnemy2 = 0;
    [SerializeField]
    private int incrementEnemy2 = 1;
    
    void InitiateWave()
    {
        for (int i = 0; i < baseEnemy1 + (incrementEnemy1 * wave); i++)
        {
            if (baseEnemy1 + (incrementEnemy1 * wave) <= 0) break;
            Debug.Log("spawn 1");
            SpawnEnemy(PoolManager.Instance.poolEnemies[0]);
        }
        for (int i = 0; i < baseEnemy2 + (incrementEnemy2 * wave); i++)
        {
            if (baseEnemy2 + (incrementEnemy2 * wave) <= 0) break;
            Debug.Log("spawn 2");
            SpawnEnemy(PoolManager.Instance.poolEnemies[1]);
        }
    }
    bool CheckIfAllDead()
    {
        bool toReturn = false;
        if (PoolManager.Instance.poolEnemies[0].CheckIfAllDisabled() &&
            PoolManager.Instance.poolEnemies[1].CheckIfAllDisabled())
            toReturn = true;
        return toReturn;
    }
    void SpawnEnemy(ObjectPooler pool)
    {
        ScreenEdgeReference reference = ScreenEdgeReference.Instance;
        float randomX = 0;
        if (Random.Range(0, 2) == 0)
        {
            randomX = reference.GetLeft().x;
        }
        else
        {
            randomX = reference.GetRight().x;
        }

        float randomY = Random.Range(reference.GetBotton().y, reference.GetTop().y);

        GameObject enemy = PoolManager.Instance.GrabFromPool(pool);
        enemy.SetActive(true);
        enemy.transform.SetPositionAndRotation(new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
    void Awake()
    {
        InitiateWave();
    }

    void Update()
    {
        if (CheckIfAllDead())
        {
            wave++;
            Debug.Log("Next Wave " + wave);
            InitiateWave();
        }


    }
}
