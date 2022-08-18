using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTileOnCollision : MonoBehaviour
{
    [Header("References")] public GameObject balls;
    [SerializeField] private Transform bonusBalltileText;
    private Transform ballTile;
    private bool collided;
    public Rigidbody2D ball;
    private Vector3 velocity;
    private bool triggered;
    private void Start()
    {
        ballTile = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            BallBehavior.ballCount++;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other);
            triggered = true;
        }
    }


    private void TriggerBonusAnimation()
    {
        if (triggered)
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                bonusBalltileText.position, ref velocity, 0.7f);
            Destroy(gameObject, 3f);
        }
    }
    private void Update()
    {
        if (!PlayCanvasConfig.gameIsPaused && !PlayCanvasConfig.gameOver)
        {
            TriggerBonusAnimation();
        }
    }
}
