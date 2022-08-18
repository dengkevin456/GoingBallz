using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGiftShooter : MonoBehaviour
{
    [Header("References")]
    public Camera playerCam;
    public Rigidbody2D ball;
    public GameObject shooterTip;
    public Transform obstacleGroup;
    [Header("Ball shoot settings")] public float ballSpeed;
    public int ballCount = 10;
    public float reloadSpeed = 0.4f;
    private Transform shooter;
    private bool isShooting;
    private float timer;
    private float power;
    private float frameCount;
    void Start()
    {
        shooter = GetComponent<Transform>();
    }

    private void LookAtMouse()
    {
        if (!isShooting)
        {
            Vector2 mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mousePosition.y - shooter.position.y, mousePosition.x - shooter.position.x)
                          * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, 0f, 120f);
            shooter.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void IgnoreCollision()
    {
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    private void ObstacleGroupVariation()
    {
        if (frameCount > 100) frameCount = 0;
        frameCount += Time.deltaTime;
    }

    private void Shooting()
    {
        if (!isShooting && Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
            power = Vector2.Distance(mousePosition, shooterTip.transform.position);
            ballCount = 5;
            isShooting = true;
        }

        if (isShooting && ballCount > 0)
        {
            if (timer > 4)
            {
                Rigidbody2D newBall = Instantiate(ball);
                newBall.position = shooterTip.transform.position;
                newBall.velocity = shooter.right * (ballSpeed * power);
                ballCount--;
                timer = 0;
            }
            timer += reloadSpeed * Time.deltaTime;
        }

        if (ballCount <= 0) isShooting = false;
    }
    void Update()
    {
        Shooting();
        IgnoreCollision();
        LookAtMouse();
        ObstacleGroupVariation();
    }

    private void FixedUpdate()
    {
        obstacleGroup.transform.position = new Vector3(-1 + Mathf.Sin(frameCount) * 0.7f * Mathf.Rad2Deg * Time.deltaTime, 
            obstacleGroup.transform.position.y);
    }
}
