using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISpace
{
    public sealed class UIAmmo
    {
        private ImageSingleton ammoCircleSingleton;
        private TextMeshProSingleton ammoTextSingleton;

        private Image ammoCircle;

        private TextMeshProUGUI ammoText;
        private TextMeshProUGUI magText;

        public UIAmmo()
        {
            ammoCircleSingleton = ImageSingleton.Instance;
            ammoTextSingleton = TextMeshProSingleton.Instance;
        }

        public void SetReferences()
        {
            ammoCircle = ammoCircleSingleton.AmmoAmountInCircle;

            ammoText = ammoTextSingleton.AmmoQuantityInText;
            magText = ammoTextSingleton.MagQuantityInText;
        }

        public void SetAmmoCount(float ammoCount, int maxAmmoCount)
        {
            float fillAmount = ammoCount / maxAmmoCount;
            ammoCircle.fillAmount = fillAmount;
        }

        public void ReloadAmmoIndicator(int magCount, int ammoCount, int ammoMaxCount)
        {
            magText.text = magCount.ToString();
            ammoText.text = $"{ammoCount}/{ammoMaxCount}";
        }

        public void SetAmmoCircleColor(Color color) => ammoCircle.color = color;
    }
}