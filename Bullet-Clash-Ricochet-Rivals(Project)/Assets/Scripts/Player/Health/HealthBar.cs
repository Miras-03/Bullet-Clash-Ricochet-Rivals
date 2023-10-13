using UnityEngine;
using Zenject;

public class HealthBar : MonoBehaviour, IHealthObserver
{
    private Health playerHealth;

    [SerializeField] private RectTransform healthBG;
    private float originalHealthBarSize;

    [Inject]
    public void Contruct(Health health) => playerHealth = health;

    private void Start() => originalHealthBarSize = healthBG.sizeDelta.x;

    public void OnHealthChanged(int damageValue) =>
        healthBG.sizeDelta = new Vector2(originalHealthBarSize * playerHealth.TakeDamage/100, healthBG.sizeDelta.y);
}