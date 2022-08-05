using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTileOnCollision : MonoBehaviour
{
    [Header("References")] public GameObject balls;
    private Transform ballTile;
    private bool collided;
    public Rigidbody2D ball;
    public LayerMask ballMask = 3;
    private void Start()
    {
        ballTile = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            BallBehavior.ballCount++;
            Debug.LogWarning($"Count: {BallBehavior.localBallCount}");
            Debug.LogWarning($"Bruh: {BallBehavior.bulletLength}");
            Destroy(gameObject);
        }
    }
}
