using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private int shipMaxHealth = 5;
    private int shipHealth = 0;
    private int shipLevel = 0;
    private int shipXP = 0;
    [SerializeField]
    private ShipLevel[] levels;
    [SerializeField]
    [Range(0.01f, 2f)]
    private float shootCooldown = 0.1f;
    
    public int GetShipHealth()
    {
        return shipHealth;
    }
    public void ApplyDamage()
    {
        shipHealth -= 1;
        if (shipHealth <= 0)
            SceneChangeManager.Instance.LoadMainScene();
    }
    public void ChangeColor()
    {
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        renderer.color = levels[shipLevel].GetColor();
    }
    public void IncrementXP(int amount)
    {
        shipXP += amount;

        if (shipXP >= levels[shipLevel].GetRequiredXP())
            LevelUp();
    }
    void LevelUp()
    {
        Debug.Log("LVL UP");
        shipXP = 0;
        if (shipLevel >= levels.Length - 1) return;
        shipLevel += 1;
        LevelController.Instance.ChangeShipLevel(levels[shipLevel]);
        ChangeColor();
    }

    private void Awake()
    {
        ChangeColor();
        shipHealth = shipMaxHealth;
    }

}
