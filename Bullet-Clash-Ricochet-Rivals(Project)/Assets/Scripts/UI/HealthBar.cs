using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private TextMeshProUGUI healthBar;

    public void OnHealthChanged(int damageValue) => healthBar.text = damageValue.ToString();
}
