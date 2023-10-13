using TMPro;
using UnityEngine;

public class TextMeshProUGUISingleton : MonoBehaviour
{
    public static TextMeshProUGUISingleton Instance { get; private set; }

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;

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