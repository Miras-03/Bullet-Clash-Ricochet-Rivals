using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISpace
{
    public sealed class UIAmmo
    {
        private AmmoCircleSingleton ammoCircleSingleton;
        private AmmoTextSingleton ammoTextSingleton;

        private Image ammoCircle;

        private TextMeshProUGUI ammoText;
        private TextMeshProUGUI magText;

        public UIAmmo()
        {
            ammoCircleSingleton = AmmoCircleSingleton.Instance;
            ammoTextSingleton = AmmoTextSingleton.Instance;
        }

        public void SetReferences()
        {
            ammoCircle = ammoCircleSingleton.GetAmmoQuantity;

            ammoText = ammoTextSingleton.GetAmmoQuantityInText;
            magText = ammoTextSingleton.GetMagQuantityInText;
        }

        public void SetAmmo(float ammoCount, int maxAmmoCount, Color color)
        {
            float fillAmount = ammoCount / maxAmmoCount;
            ammoCircle.fillAmount = fillAmount;
            ammoCircle.color = color;
        }

        public void ReloadAmmoIndicator(int magCount, int ammoCount, int ammoMaxCount)
        {
            magText.text = magCount.ToString();
            ammoText.text = $"{ammoCount}/{ammoMaxCount}";
        }
    }
}