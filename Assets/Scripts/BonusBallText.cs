using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BonusBallText : MonoBehaviour
{
    private TextMeshPro bonusBallText;

    private void Start()
    {
        bonusBallText = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        bonusBallText.text = $"x{BallBehavior.ballCount}";
    }
}
