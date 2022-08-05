using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    [Header("References")]
    public LayerMask ballMask;
    public GameObject floor;
    private void IgnoreCollision()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    private void Update()
    {
        IgnoreCollision();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == floor.gameObject)
        {
            if (BallBehavior.localBallCount > 0)
                BallBehavior.localBallCount -= 1;
            Debug.Log(BallBehavior.localBallCount);
            Destroy(gameObject);
        }
    }
}
