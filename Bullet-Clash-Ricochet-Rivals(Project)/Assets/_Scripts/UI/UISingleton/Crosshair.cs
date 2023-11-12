using UnityEngine;
using UnityEngine.UI;

public sealed class Crosshair : MonoBehaviour
{
    public static Crosshair Instance;

    [SerializeField] private Image[] images;
    private int crosshairIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SwitchCrosshair()
    {
        images[crosshairIndex].enabled = true;
        SetCrosshairIndex();
        images[crosshairIndex].enabled = false;
    }

    public void EnableCrosshairs()
    {
        foreach (Image image in images)
            image.enabled = true;
    }

    public void DisableCrosshairs()
    {
        foreach (Image image in images) 
            image.enabled = false;
    }

    private void SetCrosshairIndex() => crosshairIndex = (crosshairIndex + 1) % images.Length;
}
