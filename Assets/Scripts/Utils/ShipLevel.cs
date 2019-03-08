using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLevel : MonoBehaviour
{

    [SerializeField]
    private int xpRequiredToLevelUp = 500;
    [SerializeField]
    private int shotDamage = 1;
    //Each level is represented by the ship color in this case but
    //could also be a sprite change
    [SerializeField]
    private Color shipColor;

    public Color GetColor()
    {
        return shipColor;
    }
    public int GetShotDamage()
    {
        return shotDamage;
    }
    public int GetRequiredXP()
    {
        return xpRequiredToLevelUp;
    }

}
