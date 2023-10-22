using TMPro;
using UnityEngine.UI;

public class DisplayAmmo
{
    private Image ammoCircle;

    private TextMeshProUGUI ammoText;
    private TextMeshProUGUI magText;

    public void SetReferences()
    {
        ammoCircle = UIAmmoSingleton.Instance.uiImage;

        ammoText = TextMeshProUGUISingleton.Instance.ammoText;
        magText = TextMeshProUGUISingleton.Instance.magText;
    }

    public void SetAmmo(float ammoCount, int maxAmmoCount)
    {
        float fillAmount = ammoCount / maxAmmoCount;
        ammoCircle.fillAmount = fillAmount;
    }

    public void ReloadAmmoIndicator(int magCount, int ammoCount, int ammoMaxCount)
    {
        magText.text = magCount.ToString();
        ammoText.text = $"{ammoCount}/{ammoMaxCount}";
    }
}
