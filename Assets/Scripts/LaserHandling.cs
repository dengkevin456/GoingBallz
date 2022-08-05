using UnityEngine;
public class LaserHandling : MonoBehaviour
{
    [Header("References")]
    public LayerMask laserMask = 6;

    private void IgnoreCollision()
    {
        Physics2D.IgnoreLayerCollision(3, 6);
    }
    private void FixedUpdate()
    {
        IgnoreCollision();
    }
}