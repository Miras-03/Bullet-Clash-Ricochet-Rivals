using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    [Header("Scripts")]
    protected CameraShake cameraShake;
    protected DisplayAmmo displayAmmo;
    protected BulletDestroy bulletDestroy;

    public Coroutine reloadCoroutine;

    protected int ammo;
    protected int mag;
    protected int spareAmmo;
    protected int maxAmmoCount;

    protected int shootSpeed = 20;

    protected const string Bullet = nameof(Bullet);
    protected const string ReloadGun = nameof(ReloadGun);

    protected const float magnitude = 0.4f;
    protected const float shakeDuration = 0.2f;

    [HideInInspector] public bool isReloading = false;
    protected bool isCanceled = false;

    private void Awake()
    {
        cameraShake = GetComponentInParent<CameraShake>();
        displayAmmo = GetComponentInParent<DisplayAmmo>();
        bulletDestroy = GetComponentInParent<BulletDestroy>();
    }

    public abstract void FireAsync();
    public abstract void Reload();
    public abstract void TurnReloading(bool state);
    protected abstract void ReloadAmmo();
    public abstract void SetUIOfAmmo();
    public abstract void ReloadAmmoIndicator();
    public abstract void StopReloadCoroutine();
    public abstract void ResetAnimator();
    public abstract IEnumerator WaitForReload();
}
