using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [Header("References")]
    public LayerMask ballMask;
    public GameObject floor;
    private void IgnoreCollision()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void ClampYVelocity()
    {
        if (Mathf.Abs(rb.velocity.y) < 1.2f)
        {
            if (rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, 1.2f);
            else rb.velocity = new Vector2(rb.velocity.x, -1.2f);
        }
    }

    private void Update()
    {
        IgnoreCollision();
        ClampYVelocity();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == floor.gameObject)
        {
            if (BallBehavior.localBallCount > 0)
                BallBehavior.localBallCount--;
            Debug.Log(BallBehavior.localBallCount);
            Destroy(gameObject);
        }
    }
}
