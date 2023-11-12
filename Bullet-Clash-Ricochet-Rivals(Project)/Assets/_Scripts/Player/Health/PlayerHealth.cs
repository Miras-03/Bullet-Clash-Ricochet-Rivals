using Photon.Pun;
using System.Collections;
using UISpace;
using UnityEngine;
using HealthSpace;

namespace PlayerSpace
{
    public sealed class PlayerHealth : MonoBehaviour, IDieable
    {
        private HealthManager healthManager;
        private PhotonView photonView;

        private const int damageValue = 25;
        private const int maxHealthValue = 100;

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
            healthManager = FindObjectOfType<HealthManager>();
        }

        private void Start() => healthManager.SetMaxHealthValue(maxHealthValue);

        public void PerformMurder() => photonView.RPC("PerformGameOver", RpcTarget.All);

        [PunRPC]
        public void TakeDamage() => healthManager.TakeDamage(damageValue);
    }
}