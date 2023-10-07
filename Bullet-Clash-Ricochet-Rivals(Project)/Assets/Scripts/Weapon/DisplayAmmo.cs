using TMPro;
using UnityEngine;

public class DisplayAmmo : MonoBehaviour
{
    private TextMeshProUGUI ammoText;
    private TextMeshProUGUI magText;

    private void Awake()
    {
        TextMeshProUGUIManager textManager = TextMeshProUGUIManager.Instance;

        if (textManager != null)
        {
            ammoText = textManager.ammoText;
            magText = textManager.magText;
        }
        else
            Debug.LogError("TextMeshProUGUIManager not found. Make sure it is in the scene.");
    }

    public void ReloadAmmoIndicator(int magCount, int ammoCount, int ammoMaxCount)
    {
        magText.text = magCount.ToString();
        ammoText.text = $"{ammoCount}/{ammoMaxCount}";
    }
}
