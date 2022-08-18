using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileInstance : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPos;
    public Transform ballTile;
    public GameObject moneyTilePrefab;
    private Transform tileGroup;
    public static int overAllCount = 1;
    public Transform tile;
    private void Start()
    {
        tileGroup = GetComponent<Transform>();
        tile.gameObject.SetActive(false);
    }

    private void GenerateTile()
    {
        for (int i = 0; i < 5; i++)
        {
            int r = Random.Range(0, 10);
            if (r <= 3)
            {
                Transform tilePos = spawnPos.transform;
                Transform newTile = Instantiate(tile, tileGroup);
                newTile.gameObject.SetActive(true);
                newTile.position = new Vector3(tilePos.position.x + i * tile.localScale.x, 
                    tilePos.position.y, 0);
            }

            if (r == 4)
            {
                Transform tilePos = spawnPos.transform;
                Transform newBallTile = Instantiate(ballTile, tileGroup);
                newBallTile.position = new Vector3(tilePos.position.x + i * tile.localScale.x, tilePos.position.y, 0);
            }

            if (r == 5)
            {
                Transform tilePos = spawnPos.transform;
                GameObject newMoneyTile = Instantiate(moneyTilePrefab, tileGroup);
                newMoneyTile.transform.position = new Vector3(tilePos.position.x + i * tile.localScale.x,
                    tilePos.position.y, 0);
            }
        }
    }
    private void Update()
    {
        if (!PlayCanvasConfig.gameIsPaused && !PlayCanvasConfig.gameOver)
        {
            if (BallBehavior.localBallCount <= 0 && BallBehavior.bulletLength <= 0 && BallBehavior.locked)
            {
                GenerateTile();
                overAllCount++;
            }
        }
    }
    


}
