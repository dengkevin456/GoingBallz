using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
public class TileBehavior : MonoBehaviour
{
    [Header("References")] public TextMeshPro tileText;
    public GameObject floor;
    [Header("Camera shaking")] public float duration = 0.4f;
    public AnimationCurve curve;
    [Header("Tile stuff")]
    public Transform tileGroup;
    public Transform tile;
    private Vector3 velocity;
    private int tileCount;
    private bool destroyed;

    private void Start()
    {
        tileCount = Random.Range(TileInstance.overAllCount, TileInstance.overAllCount + 3);
    }

    private void HandleTileText()
    {
        tileText.text = $"{tileCount}";
    }

    public void TileYMovement()
    {
        if (BallBehavior.locked && BallBehavior.bulletLength <= 0 && BallBehavior.localBallCount <= 0)
        {
            Transform tilePos = tile.transform;
            /*
             * tilePos.position = Vector3.SmoothDamp(tilePos.position, 
                tilePos.position + Vector3.down * tilePos.localScale.y,
                ref velocity, 0.4f);
             */
            Debug.Log("Lol!");
            /****
             tileGroup.position = Vector3.Lerp(tileGroup.position,
                tileGroup.position + Vector3.down * (tilePos.localScale.y * 2), 0.1f);
             */
            tileGroup.position += Vector3.down * (tilePos.localScale.y);
        }
    }


    private void DestroyTile()
    {
        if (tileCount <= 0)
        {
            // Add some particle effects here
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!PlayCanvasConfig.gameIsPaused)
        {
            HandleTileText();
            DestroyTile();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            tileCount--;
        }

        if (other.gameObject == floor && !PlayCanvasConfig.gameOver)
        {
            Debug.Log("Collision!");
            PlayCanvasConfig.gameOver = true;
        }
    }
}
