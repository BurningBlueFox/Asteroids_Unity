using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2FSM : MonoBehaviour
{
    [SerializeField]
    RadialBlasterController blaster;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    int maxHealth = 10;
    int health = 0;
    [SerializeField]
    [Range(0.5f, 5f)]
    private float knockbackMult = 5f;
    [SerializeField]
    int xpValue = 300;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float speedMult = 1.5f;
    [SerializeField]
    [Range(0.01f, 2f)]
    private float shootCooldown = 1f;
    private float cooldownTime = 0f;

    private States state;
    enum States
    {
        Idle,
        Shoot,
        Die
    }
    void Awake()
    {
        health = maxHealth;
        state = States.Idle;
        if (body == null) body = this.gameObject.GetComponent<Rigidbody2D>();
        if (blaster == null) blaster = this.gameObject.GetComponent<RadialBlasterController>();
        cooldownTime = 0;
    }

    void Update()
    {
        switch (state)
        {
            case States.Idle:
                IdleState();
                break;
            case States.Shoot:
                ShootState();
                break;
            case States.Die:
                DieState();
                break;
            default:
                break;
        }
    }

    private void IdleState()
    {
        state = States.Shoot;
    }
    private void ShootState()
    {
        body.velocity = new Vector2(-speedMult, 0);
        cooldownTime += Time.deltaTime;
        if (cooldownTime >= shootCooldown)
        {
            cooldownTime = 0f;
            blaster.Shoot();
        }
    }
    private void DieState()
    {
        LevelController.Instance.AddPlayerExperience(xpValue);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            collision.gameObject.SetActive(false);
            int damage = LevelController.Instance.GetPlayerShotDamage();
            health -= damage;
            if (health <= 0) state = States.Die;
        }
    }

}
