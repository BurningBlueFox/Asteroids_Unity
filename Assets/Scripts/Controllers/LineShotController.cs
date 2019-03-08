using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShotController : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 2f)]
    private float velocity = 1f;
    [SerializeField]
    private Rigidbody2D body;
    private ScreenEdgeReference edge;

    private void ThrustForward()
    {
        body.velocity = new Vector2(0, 0);
        body.AddRelativeForce(new Vector2(0, velocity), ForceMode2D.Impulse);
        
    }
    void Awake()
    {
        if (body == null) body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, 0);
        edge = ScreenEdgeReference.Instance;
        //ThrustForward();
    }
    private void Update()
    {
        ThrustForward();
        CheckIfOffscreen();
    }
    private void CheckIfOffscreen()
    {
        if (this.transform.position.y > edge.GetTop().y) this.gameObject.SetActive(false);
        if (this.transform.position.y < edge.GetBotton().y) this.gameObject.SetActive(false);
        if (this.transform.position.x < edge.GetLeft().x) this.gameObject.SetActive(false);
        if (this.transform.position.x > edge.GetRight().x) this.gameObject.SetActive(false);
    }
}
