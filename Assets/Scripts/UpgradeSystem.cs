using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public static int currentTier = 0;
    [System.Serializable]
    public class UpgradeAttributes
    {
        public string name { get; set; }
        public float reloadSpeed { get; set; }
        public float inaccuracy { get; set; }
        public Image cannonImage { get; set; }

        public UpgradeAttributes(string name, float reloadSpeed, float inaccuracy, Image cannonImage)
        {
            this.name = name;
            this.reloadSpeed = reloadSpeed;
            this.inaccuracy = inaccuracy;
            this.cannonImage = cannonImage;
        }
    }

    public static List<UpgradeAttributes> upgrades = new List<UpgradeAttributes>()
    {
        new UpgradeAttributes("Tier 1", 0.7f, 0.1f, null)
    };
}
