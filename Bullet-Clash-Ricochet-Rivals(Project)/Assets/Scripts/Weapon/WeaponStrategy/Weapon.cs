using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] protected CameraShake cameraShake;
    [SerializeField] protected DisplayAmmo displayAmmo;

    protected int ammo;
    protected int mag;
    protected int spareAmmo;
    protected int maxAmmoCount;

    protected int shootSpeed = 20;

    protected const string Bullet = nameof(Bullet);

    protected const float magnitude = 0.4f;
    protected const float shakeDuration = 0.2f;

    [HideInInspector] public bool isReloading = false;
    protected bool isCanceled = false;

    public abstract void FireAsync();
    public abstract void Reload();
    protected abstract void ReloadAmmo();
    protected abstract void TurnReloading(bool state);
    protected abstract IEnumerator DestroyBullet(GameObject bullet);
    protected abstract IEnumerator WaitForReload();
}
