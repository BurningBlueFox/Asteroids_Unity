using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1FSM : MonoBehaviour
{
    [SerializeField]
    BlasterController blaster;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    int maxHealth = 5;
    int health = 0;
    [SerializeField]
    int xpValue = 100;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float speedMult = 1.5f;
    [SerializeField]
    [Range(0.5f, 5f)]
    private float knockbackMult = 5f;
    [SerializeField]
    [Range(0.01f, 2f)]
    private float shootCooldown = 1f;
    private float cooldownTime = 0f;

    [SerializeField]
    [Range(0.5f, 3f)]
    float distToShoot;

    private States state;
    enum States
    {
        Idle,
        SeekPlayer,
        Shoot,
        Die
    }
    void Awake()
    {
        state = States.Idle;
        if (body == null) body = this.gameObject.GetComponent<Rigidbody2D>();
        if (blaster ==  null) blaster = this.gameObject.GetComponent<BlasterController>();
        cooldownTime = 0;
        health = maxHealth;
    }

    void Update()
    {
        switch (state)
        {
            case States.Idle:
                IdleState();
                break;
            case States.SeekPlayer:
                SeekPlayerState();
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
        state = States.SeekPlayer;
    }
    private void SeekPlayerState()
    {
        Vector3 playerPos = LevelController.Instance.GetPlayerPosition();

        Vector3 diff = playerPos - transform.position;
        diff.Normalize();

        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z - 90);
        body.AddRelativeForce(new Vector2(0f, speedMult), ForceMode2D.Force);

        if (Vector3.Distance(this.gameObject.transform.position, playerPos) < distToShoot)
            state = States.Shoot;
    }
    private void ShootState()
    {
        body.velocity = new Vector2(0, 0);
        Vector3 playerPos = LevelController.Instance.GetPlayerPosition();
        Vector3 diff = playerPos - transform.position;
        diff.Normalize();

        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z - 90);
        cooldownTime += Time.deltaTime;
        if (cooldownTime >= shootCooldown)
        {
            cooldownTime = 0f;
            blaster.Shoot();
        }
        if (Vector3.Distance(this.gameObject.transform.position, playerPos) > distToShoot)
            state = States.SeekPlayer;
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
            float angleOfCollision = Vector2.Angle(collision.gameObject.transform.position,
                                body.position);
            ApplyKnockback(angleOfCollision);
            if (health <= 0) state = States.Die;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float angleOfCollision = Vector2.Angle(collision.GetContact(0).point,
                                body.position);
        ApplyKnockback(angleOfCollision);

    }
    private void ApplyKnockback(float angle)
    {
        float radians = (Mathf.PI / 180f) * angle;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        body.AddForce(new Vector2(cos * knockbackMult, sin * knockbackMult),
            ForceMode2D.Impulse);
    }
}
