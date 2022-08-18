using UnityEngine;
public class LaserHandling : MonoBehaviour
{
    [Header("References")] public Transform tile;
    private void IgnoreCollision()
    {
        Physics2D.IgnoreLayerCollision(3, 6);
    }
    private void FixedUpdate()
    {
        IgnoreCollision();
    }

    private void Update()
    {
        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        if (tile.position.y + tile.localScale.y / 2 <= transform.position.y)
        {
            PlayCanvasConfig.gameOver = true;
        }
    }
    
    
}