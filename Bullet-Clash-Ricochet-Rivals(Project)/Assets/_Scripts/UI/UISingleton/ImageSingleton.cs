using UnityEngine;
using UnityEngine.UI;

namespace UISpace
{
    public sealed class ImageSingleton : MonoBehaviour
    {
        public static ImageSingleton Instance { get; private set; }

        [Header("UI images")]
        [SerializeField] private Image ammoAmountInCircle;
        [SerializeField] private Image ammoBGInCircle;
        [SerializeField] private Image healthAmount;
        [SerializeField] private Image healthBG;
        [SerializeField] private Image gameOverPanel;

        public Image AmmoAmountInCircle { get => ammoAmountInCircle; }
        public Image AmmoBGInCircle { get => ammoBGInCircle; }
        public Image HealthAmount { get => healthAmount; }
        public Image HealthBG { get => healthBG; }
        public Image GameOverPanel { get => gameOverPanel; }

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

        public void SetGameOverPanelColor(Color color) => GameOverPanel.color = color;
    }
}