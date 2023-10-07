using TMPro;
using UnityEngine;

public class TextMeshProUGUIManager : MonoBehaviour
{
    public static TextMeshProUGUIManager Instance { get; private set; }

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}