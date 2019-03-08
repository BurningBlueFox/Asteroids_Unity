using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEdgeReference : MonoBehaviour
{
    [SerializeField]
    private GameObject top, botton, left, right;
    private Vector2 edge_top, edge_botton, edge_left, edge_right;
    //Singleton class
    public static ScreenEdgeReference Instance { get; private set; }

    public Vector2 GetTop()
    {
        return edge_top;
    }
    public Vector2 GetBotton()
    {
        return edge_botton;
    }
    public Vector2 GetLeft()
    {
        return edge_left;
    }
    public Vector2 GetRight()
    {
        return edge_right;
    }
    private void SetEdges()
    {
        edge_top = new Vector2(top.transform.position.x,
                               top.transform.position.y);
        edge_botton = new Vector2(botton.transform.position.x,
                               botton.transform.position.y);
        edge_left = new Vector2(left.transform.position.x,
                               left.transform.position.y);
        edge_right = new Vector2(right.transform.position.x,
                               right.transform.position.y);
    }
    void Awake()
    {
        //Initiate Singleton and overwrite if one exists already
        Instance = this;

        SetEdges();
    }

}
