using UnityEngine;
using UnityEngine.UI;

public class UIAmmoSingleton : MonoBehaviour
{
    public static UIAmmoSingleton Instance { get; private set; }

    public Image uiImage;

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

    public Image GetImage { get => uiImage; set => uiImage = value; }
}
