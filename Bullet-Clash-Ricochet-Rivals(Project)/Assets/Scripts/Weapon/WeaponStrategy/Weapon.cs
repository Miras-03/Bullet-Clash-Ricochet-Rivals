using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    protected CameraShake cameraShake;
    protected DisplayAmmo displayAmmo;
    protected BulletDestroy bulletDestroy;

    protected GameObject bulletPrefab;
    protected Rigidbody bulletRB;

    [Space(20)]
    [Header("PlayerBelong")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Animator reloadAnimator;

    [HideInInspector] public Coroutine reloadCoroutine;

    protected int ammo;
    protected int mag;
    protected int spareAmmo;
    protected int maxAmmoCount;

    protected int shootSpeed = 20;

    protected int destroyTime = 3;

    protected const string Bullet = nameof(Bullet);
    protected const string ReloadWeapon = nameof(ReloadWeapon);

    protected float magnitudeOfCamera = 0.4f;
    protected const float shakeDuration = 0.2f;

    [HideInInspector] public bool isReloading = false;
    protected bool isCanceled = false;

    private void Awake()
    {
        cameraShake = GetComponentInParent<CameraShake>();
        bulletDestroy = GetComponentInParent<BulletDestroy>();

        SetDisplayAmmo();
    }

    private void Start()
    {
        SetValues();
        SetUIOfAmmo();
        ReloadAmmoIndicator();
    }

    private void SetDisplayAmmo()
    {
        displayAmmo = new DisplayAmmo();
        displayAmmo.SetReferences();
    }

    public abstract void FireAsync();
    public abstract void Reload();
    public abstract void TurnReloading(bool state);
    protected abstract void ReloadAmmo();
    protected abstract void SetValues();
    public abstract void SetUIOfAmmo();
    public abstract void ReloadAmmoIndicator();
    public abstract void StopReloadCoroutine();
    public abstract void ResetAnimator();
    public abstract IEnumerator WaitForReload();
}
