using UnityEngine;
using UnityEngine.UI;

namespace UISpace
{
    public sealed class AmmoCircleSingleton : MonoBehaviour
    {
        public static AmmoCircleSingleton Instance { get; private set; }

        [SerializeField] private Image ammoQuantityInCircle;

        public Image GetAmmoQuantity { get => ammoQuantityInCircle; }

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