using PlayerSpace;
using UnityEngine;
using Zenject;

namespace HealthSpace
{
    public sealed class HealthManager : MonoBehaviour, IRoomObserver
    {
        private Health health;
        private PlayerHealth playerHealth;
        [SerializeField] private HealthBar healthBar;

        [Inject]
        public void Construct(Health health) => this.health = health;

        public void TakeDamage(int damageAmount) => health.TakeDamage -= damageAmount;
        public void SetMaxHealthValue(int value) => health.TakeDamage = value;

        private void GetReferenceOfObservers()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            AddObservers();
        }

        private void AddObservers()
        {
            health.AddHealthObserver(healthBar);
            health.AddDieableObserver(playerHealth);
        }

        private void OnDisable()
        {
            health.RemoveHealthObservers();
            health.RemoveDieableObservers();
        }

        public void Execute() => GetReferenceOfObservers();
    }
}