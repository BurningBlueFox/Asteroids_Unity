using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBlasterController : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private ObjectPooler bulletPool;
    [SerializeField]
    int numOfBullets = 8;
    [SerializeField]
    private int typeOfBullet = 0;

    public void Awake()
    {
        if (typeOfBullet == 0 && bulletPool == null)
            bulletPool = PoolManager.Instance.poolPlayerShots[0];
        if (typeOfBullet == 1 && bulletPool == null)
            bulletPool = PoolManager.Instance.poolEnemyShots[0];

    }
    public void Shoot()
    {
        for (int i = 0; i < numOfBullets; i++)
        {
            float angleSnap = 360f / numOfBullets;
            float angle = (angleSnap * i);
            GameObject bullet = PoolManager.Instance.GrabFromPool(bulletPool);
            ActivateBullet(bullet);
            bullet.transform.SetPositionAndRotation(muzzle.position,
                   Quaternion.identity);
            Quaternion quat = Quaternion.Euler(0, 0, angle);
             bullet.transform.rotation = quat;
        }
    }

    private void ActivateBullet(GameObject bulletToActivate)
    {
        bulletToActivate.SetActive(true);
        bulletToActivate.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
    }
}
