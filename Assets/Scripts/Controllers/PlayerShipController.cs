using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float speedMult = 1.5f;
    [SerializeField]
    [Range(0.1f, 3f)]
    private float turnMult = 1.8f;
    [SerializeField]
    [Range(0.5f, 5f)]
    private float knockbackMult = 5f;
    [SerializeField]
    [Range(0.1f, 2f)]
    private float invincibilityTime = 5f;
    private float invicibleTime = 0f;
    [SerializeField]
    [Range(0.01f, 2f)]
    private float shootCooldown;
    private float cooldownTime = 0f;
    float xAxis;
    float yAxis;
    bool shootPrompt;
    private ShipLevel level;
    private BlasterController blaster;
    private PlayerStatus status;


    private void GrabInput()
    {
        //Grab axis from PlayerInputManager
        PlayerInputManager input = PlayerInputManager.Instance;
        //In this case since the input is coming from keyboard it is not
        //a float variable but we are converting to a float on this class
        xAxis = (float)input.GetHorizontalAxis();
        yAxis = (float)input.GetVerticalAxis();
        shootPrompt = input.GetShootPrompt();
    }
    private void ApplyThrust(float value)
    {
        body.AddRelativeForce(new Vector2(0f, value * speedMult));
    }
    private void ApplyRotation(float value)
    {
        body.AddTorque(-value * turnMult);
        body.angularVelocity = 0f;
    }
    private void ApplyKnockback(float angle)
    {
        float radians = (Mathf.PI / 180f) * angle;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        body.AddForce(new Vector2(cos * knockbackMult, sin * knockbackMult),
            ForceMode2D.Impulse);
    }
    private void Shoot()
    {
        if (!shootPrompt) return;
        if(cooldownTime >= shootCooldown)
        {
            cooldownTime = 0f;
            blaster.Shoot();
        }
    }

    void Awake()
    {
        if (body == null)
            body = this.gameObject.GetComponent<Rigidbody2D>();
        if (status == null)
            status = GetComponent<PlayerStatus>();
        invicibleTime = invincibilityTime;
        cooldownTime = shootCooldown;
        blaster = this.gameObject.GetComponent<BlasterController>();
        level = this.gameObject.GetComponent<ShipLevel>();
    }

    void Update()
    {
        GrabInput();
        if(yAxis > 0)
        ApplyThrust(yAxis);
        ApplyRotation(xAxis);
        Shoot();

        invicibleTime += Time.deltaTime;
        cooldownTime += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if(invicibleTime > invincibilityTime)
        {
            float angleOfCollision = Vector2.Angle(collision.GetContact(0).point,
                                body.position);
            ApplyKnockback(angleOfCollision);

            invicibleTime = 0f;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invicibleTime > invincibilityTime && collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("Damage");
            invicibleTime = 0f;
            collision.gameObject.SetActive(false);
            status.ApplyDamage();
        }

    }



}
