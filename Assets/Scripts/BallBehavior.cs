using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [Header("References")] public GameObject balls;
    public Transform ball;
    public static int ballCount = 3;

    #region GlobalVariables

    public static int localBallCount = 3;
    public static int bulletLength = 3;

    #endregion
    public Camera playerCam;
    public float bulletSpeed = 5f;
    public TileBehavior tileBehavior;
    [Header("Cannon components")] public Transform shooter;
    public Animator shooterAnim;
    public static bool locked = false;
    private Vector2 mousePosition;
    private float cannonAngle;
    private float timer = 0f;
    public float reloadSpeed = 0.8f;
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private void RotateCannon()
    {
        mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
        cannonAngle = Mathf.Atan2(mousePosition.y - shooter.position.y, mousePosition.x - shooter.position.x)
             * Mathf.Rad2Deg;
        shooter.rotation = Quaternion.Euler(0, 0, cannonAngle);
    }

    private void ClampRotation()
    {
        cannonAngle = Mathf.Clamp(cannonAngle, 30f, 150f);
    }

    private void ShootBall()
    {
        if (Input.GetMouseButtonUp(0) && !locked && localBallCount > 0)
        {
            locked = true;
        }

        if (locked)
        {
            if (timer >= 1f && bulletLength > 0)
            {
                shooterAnim.SetBool(IsShooting, true);
                Transform newBall = Instantiate(ball, balls.transform);
                newBall.gameObject.SetActive(true);
                newBall.GetComponent<Rigidbody2D>().velocity = 
                    newBall.transform.right * bulletSpeed;
                bulletLength--;
                timer = 0f;
            }

            shooterAnim.SetBool(IsShooting, false);
            timer += reloadSpeed * Time.deltaTime;
        }

        if (localBallCount <= 0 && locked && bulletLength <= 0)
        {
            tileBehavior.TileYMovement();
            localBallCount = ballCount;
            bulletLength = ballCount;
            timer = 0;
            locked = false;
        } 
    }

    
    private void Update()
    {
        if (!PlayCanvasConfig.gameIsPaused)
        {
            if (!locked) RotateCannon();
            ShootBall();
        }
    }

    private void FixedUpdate()
    {
        ClampRotation();
    }
}