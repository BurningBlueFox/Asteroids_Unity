using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeWarpController : MonoBehaviour
{
    [SerializeField]
    private Transform t;
    private ScreenEdgeReference edge;

    private void WarpToTop()
    {
        Vector3 vec = new Vector3(t.position.x, edge.GetTop().y, t.position.z);
        t.SetPositionAndRotation(vec, t.rotation);
    }
    private void WarpToBotton()
    {
        Vector3 vec = new Vector3(t.position.x, edge.GetBotton().y, t.position.z);
        t.SetPositionAndRotation(vec, t.rotation);
    }
    private void WarpToLeft()
    {
        Vector3 vec = new Vector3(edge.GetLeft().x, t.position.y, t.position.z);
        t.SetPositionAndRotation(vec, t.rotation);
    }
    private void WarpToRight()
    {
        Vector3 vec = new Vector3(edge.GetRight().x, t.position.y, t.position.z);
        t.SetPositionAndRotation(vec, t.rotation);
    }
    void Start()
    {
        edge = ScreenEdgeReference.Instance;
        t = this.gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        //Checks if position is beyond any edges
        if (t.position.y > edge.GetTop().y) WarpToBotton();
        if (t.position.y < edge.GetBotton().y) WarpToTop();
        if (t.position.x < edge.GetLeft().x) WarpToRight();
        if (t.position.x > edge.GetRight().x) WarpToLeft();
    }
}
