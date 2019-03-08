using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterController : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private ObjectPooler bulletPool;
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
        GameObject bullet =  PoolManager.Instance.GrabFromPool(bulletPool);
        if (typeOfBullet == 0)
            PaintShot(LevelController.Instance.GetShipColor(), bullet);
        ActivateBullet(bullet);
    }

    private void ActivateBullet(GameObject bulletToActivate)
    {
        bulletToActivate.SetActive(true);
        bulletToActivate.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
        bulletToActivate.GetComponent<Rigidbody2D>().SetRotation(this.transform.rotation.eulerAngles.z);
    }
    private void PaintShot(Color c, GameObject prefab)
    {
        prefab.GetComponent<SpriteRenderer>().color = c;
    }
}
