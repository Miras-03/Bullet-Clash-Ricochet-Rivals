using HealthSpace;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace PlayerSpace
{
    public sealed class PlayerHealth : MonoBehaviour, IDieable
    {
        private HealthManager healthManager;
        private SceneManager sceneManager;

        private const int damageValue = 25;
        private const int maxHealthValue = 100;

        [Inject]
        public void Constructor(SceneManager sceneManager) => this.sceneManager = sceneManager;

        private void Awake() => healthManager = FindObjectOfType<HealthManager>();

        private void Start() => healthManager.SetMaxHealthValue(maxHealthValue);

        public void PerformMurder()
        {
            sceneManager.LoadLobbyScene();
        }

        [PunRPC]
        public void TakeDamage() => healthManager.TakeDamage(damageValue);
    }
}