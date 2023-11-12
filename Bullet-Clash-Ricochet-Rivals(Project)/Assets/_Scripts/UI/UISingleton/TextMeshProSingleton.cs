using TMPro;
using UnityEngine;

namespace UISpace
{
    public sealed class TextMeshProSingleton : MonoBehaviour
    {
        public static TextMeshProSingleton Instance { get; private set; }

        [Header("UI Ammo")]
        [SerializeField] private TextMeshProUGUI ammoQuantity;
        [SerializeField] private TextMeshProUGUI magQuantity;
        [SerializeField] private TextMeshProUGUI gameOverMessage;

        public TextMeshProUGUI AmmoQuantityInText { get => ammoQuantity; }
        public TextMeshProUGUI MagQuantityInText { get => magQuantity; }
        public TextMeshProUGUI GameOverMessage { get => gameOverMessage; }

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
}