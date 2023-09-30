using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDieable
{
    private HealthManager healthManager;

    private const int damageAmount = 25;

    private void Awake()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void Start()
    {
        healthManager.SetMaxHealthValue(100);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            healthManager.TakeDamage(damageAmount);
    }

    public void PerformMurder() => Destroy(gameObject);
}
