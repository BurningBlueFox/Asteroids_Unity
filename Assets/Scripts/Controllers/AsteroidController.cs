using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private int asteroidMaxHealth = 10;
    private int asteroidHealth;

    //The quantity of the smaller asteroids
    [SerializeField]
    private int numOfChilds = 3;
    [Range(0.01f, 300f)]
    [SerializeField]
    private float childsVel = 300f;
    [SerializeField]
    private ObjectPooler pool;
    [SerializeField]
    private int asteroidType = 0;

    private void Awake()
    {
        asteroidHealth = asteroidMaxHealth;
        if (pool == null)
        {
            switch (asteroidType)
            {
                case 0:
                    pool = PoolManager.Instance.poolAsteroids[asteroidType + 1];
                    break;
                case 1:
                    pool = PoolManager.Instance.poolAsteroids[asteroidType + 1];
                    break;
                case 2:

                    break;
            }
        }
            
    }
    private void Explode()
    {
        if(pool == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < numOfChilds; i++)
            {
                float angleSnap = 360f / numOfChilds;
                float angle = (angleSnap * i);
                float radians = (Mathf.PI / 180f) * angle;
                float cos = Mathf.Cos(radians);
                float sin = Mathf.Sin(radians);
                GameObject child = PoolManager.Instance.GrabFromPool(pool);
                child.SetActive(true);
                child.transform.SetPositionAndRotation(this.gameObject.transform.position,
                   Quaternion.identity);
                Rigidbody2D body = child.GetComponent<Rigidbody2D>();
                body.AddForce(new Vector2(cos * childsVel, sin * childsVel),
                    ForceMode2D.Impulse);
            }
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            collision.gameObject.SetActive(false);
            int damage = LevelController.Instance.GetPlayerShotDamage();
            asteroidHealth -= damage;
            if (asteroidHealth <= 0) Explode();
        }
    }



}
