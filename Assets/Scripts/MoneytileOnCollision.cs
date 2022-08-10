using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoneytileOnCollision : MonoBehaviour
{
    private GameObject moneyTile;


    private void Start()
    {
        moneyTile = GetComponent<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayCanvasConfig.money += 5;
        Destroy(gameObject);
    }
}
