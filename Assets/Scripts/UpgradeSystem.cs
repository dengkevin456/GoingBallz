using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [Header("References")] public Animator upgradeCanvasAnimator;
    private static readonly int IsUpgrading = Animator.StringToHash("isUpgrading");

    [Header("Reload upgrade tier")] public int reloadUpgradeTier;
    public TextMeshProUGUI reloadUpgradeText;
    public Button reloadUpgradeButton;
    /// <summary>
    /// First value is the reload speed and second is the price, third tells
    /// when you already bought the stuff or not (0 = not bought, 1 = bought), with reloadupgradetier the index
    /// </summary>
    public readonly List<List<float>> reloadUpgrades = new List<List<float>>
    {
        new List<float>{0.8f, 0, 1},
        new List<float>{0.9f, 10f, 0},
        new List<float>{1f, 20f, 0}
    };

    /// <summary>
    /// Assign this to the reload upgrade button
    /// </summary>
    public void BuyReloadUpgrade()
    {
        if (!PlayCanvasConfig.gameIsPaused && !PlayCanvasConfig.gameOver)
        {
            if (reloadUpgradeTier < reloadUpgrades.Count - 1)
            {
                PlayCanvasConfig.money -= reloadUpgrades[reloadUpgradeTier][1];
                reloadUpgradeTier++;
            }
            else Debug.Log("You have reached the maximum iter!");
        }
    }

    private void SetUpgradeTexts()
    {
        reloadUpgradeText.text = $"Reload speed: {reloadUpgrades[reloadUpgradeTier][0]}";
        HandleBuyButton(reloadUpgrades, reloadUpgradeTier, reloadUpgradeButton);
    }

    private void HandleBuyButton(List<List<float>> upgradeType, int index, Button button)
    {
        float money = PlayCanvasConfig.money;
        if (money >= upgradeType[index][1])
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = (int) upgradeType[index][2] == 1 
                ? "Upgrade!" : $"Buy? ({upgradeType[index][1]}$)";
            button.interactable = true;
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough\nmoney!";
            button.interactable = false;
        }
    }

    public void TriggerUpgrade()
    {
        if (!PlayCanvasConfig.gameIsPaused && !PlayCanvasConfig.gameOver)
        {
            upgradeCanvasAnimator.SetBool(IsUpgrading, true);
        }
    }

    public void UntriggerUpgrading()
    {
        if (!PlayCanvasConfig.gameIsPaused && !PlayCanvasConfig.gameOver)
        {
            upgradeCanvasAnimator.SetBool(IsUpgrading, false);
        }
    }

    private void Update()
    {
        SetUpgradeTexts();
    }
}
