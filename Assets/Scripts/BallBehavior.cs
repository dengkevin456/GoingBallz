using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallBehavior : MonoBehaviour
{
    [Header("References")] public GameObject balls;
    public Transform ball;
    public static int ballCount = 3;
    public UpgradeSystem us;
    #region GlobalVariables
    
    public static int localBallCount = 3;
    public static int bulletLength = 3;
    public static float inaccuracy = .1f;
    public static float damage = 1f;

    #endregion
    public Camera playerCam;
    public float bulletSpeed = 5f;
    public TileBehavior tileBehavior;
    [Header("Cannon components")] public Transform shooter;
    public Animator shooterAnim;
    public static bool locked = false;
    private Vector2 mousePosition;
    private float cannonAngle;
    private float timer;
    public float reloadSpeed = 0.8f;
    private static readonly int IsShooting = Animator.StringToHash("isShooting");

    private void Start()
    {
        reloadSpeed = us.reloadUpgrades[us.reloadUpgradeTier][0];
        damage = us.damageUpgrades[us.damageUpgradeTier][0];
    }
    
    /// <summary>
    /// Don't forget to update your upgrades here! (reload speed, bullet speed, etc.)
    /// </summary>
    private void UpdateUpgrades()
    {
        reloadSpeed = us.reloadUpgrades[us.reloadUpgradeTier][0];
        damage = us.damageUpgrades[us.damageUpgradeTier][0];
    }
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

    private bool isDistance(float distance)
    {
        Vector2 mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
        bool isd = Vector2.Distance(mousePosition, shooter.transform.position) < distance;
        return isd;
    }

    private void ShootBall()
    {
        if (Input.GetMouseButtonUp(0) && !locked && localBallCount > 0)
        {
            if (isDistance(2f))
                locked = true;
            else Debug.Log("You have to be in the cannon's radius to shoot!");
        }

        if (locked)
        {
            if (timer >= 1f && bulletLength > 0)
            {
                shooterAnim.SetBool(IsShooting, true);
                Transform newBall = Instantiate(ball, balls.transform);
                newBall.gameObject.SetActive(true);
                newBall.GetComponent<Rigidbody2D>().velocity = 
                    (newBall.transform.right) * bulletSpeed;
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
        if (!PlayCanvasConfig.gameIsPaused && !PlayCanvasConfig.gameOver)
        {
            if (!locked) RotateCannon();
            ShootBall();
        }
    }

    private void FixedUpdate()
    {
        UpdateUpgrades();
        ClampRotation();
    }
}
