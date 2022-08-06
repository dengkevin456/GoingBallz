using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashWarningText : MonoBehaviour
{
    private TextMeshProUGUI warningText;
    private float a;

    private void Start()
    {
        warningText = GetComponent<TextMeshProUGUI>();
    }

    private void ClampTransparency()
    {
        a = Mathf.Clamp(a, 0, 255);
    }

    public void DisplayText(String text)
    {
        a = 255;
        warningText.text = text;
        
    }

    private void Update()
    {
        if (!PlayCanvasConfig.gameIsPaused)
        {
            ClampTransparency();
        }
    }
}
