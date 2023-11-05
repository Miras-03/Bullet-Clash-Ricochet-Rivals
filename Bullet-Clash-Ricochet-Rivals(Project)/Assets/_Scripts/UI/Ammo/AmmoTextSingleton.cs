using TMPro;
using UnityEngine;

public class AmmoTextSingleton : MonoBehaviour
{
    public static AmmoTextSingleton Instance { get; private set; }

    [Header("UI Ammo")]
    [SerializeField] private TextMeshProUGUI ammoQuantityText;
    [SerializeField] private TextMeshProUGUI magQuantityText;

    public TextMeshProUGUI GetAmmoQuantityInText { get => ammoQuantityText; }
    public TextMeshProUGUI GetMagQuantityInText { get => magQuantityText; }

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
}