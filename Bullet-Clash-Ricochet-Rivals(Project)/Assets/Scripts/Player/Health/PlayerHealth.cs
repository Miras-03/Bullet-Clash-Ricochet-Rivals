using Photon.Pun;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDieable
{
    private HealthManager healthManager;
    private const int damageValue = 25;
    private const int maxHealthValue = 100;
    private void Awake() => healthManager = FindObjectOfType<HealthManager>();

    private void Start() => healthManager.SetMaxHealthValue(maxHealthValue);

    public void PerformMurder() => Destroy(gameObject);

    [PunRPC]
    public void TakeDamage() => healthManager.TakeDamage(damageValue);
}