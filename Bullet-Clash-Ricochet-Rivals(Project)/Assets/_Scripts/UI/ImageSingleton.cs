using UnityEngine;
using UnityEngine.UI;

public sealed class ImageSingleton : MonoBehaviour
{
    public static ImageSingleton Instance;

    [SerializeField] private Image[] images;
    private int crosshairIndex = 0;

    public void SetCrosshair()
    {
        images[crosshairIndex].enabled = true;
        SetCrosshairIndex();
        images[crosshairIndex].enabled = false;
    }

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

    private void SetCrosshairIndex() => crosshairIndex = (crosshairIndex + 1) % images.Length;
}
