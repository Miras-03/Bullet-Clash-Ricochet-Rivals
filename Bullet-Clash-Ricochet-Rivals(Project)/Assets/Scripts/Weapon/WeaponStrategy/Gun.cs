using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [Header("BulletBelong")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private Transform firePoint;

    [Space(20)]
    [Header("Animator")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        ammo = 5;
        mag = 2;
        spareAmmo = 5;
        maxAmmoCount = 5;

        shootSpeed = 30;

        spareAmmo = ammo;
        displayAmmo.SetAmmo(ammo, maxAmmoCount);
        displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);
    }

    public override async void FireAsync()
    {
        if (ammo > 0)
        {
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetGunSound);

            ammo--;
            displayAmmo.SetAmmo(ammo, maxAmmoCount);
            displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);

            GameObject bullet = PhotonNetwork.Instantiate(Bullet, firePoint.position, firePoint.rotation);
            bulletRB.velocity = bullet.transform.forward * shootSpeed;

            if (!isCanceled)
                await cameraShake.Shake(shakeDuration, magnitude);
            StartCoroutine(DestroyBullet(bullet));
        }
        else
        {
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetLuckSound);
            if (mag>=0)
                Reload();
            else
                isCanceled = !isCanceled;
        }
    }

    public override void Reload()
    {
        bool shouldReload = (spareAmmo > 0 || mag > 0) && (ammo != spareAmmo || ammo == 0);

        if (shouldReload)
        {
            TurnReloading(true);
            animator.SetTrigger("Reload");
            StartCoroutine(WaitForReload());
        }
    }

    protected override void ReloadAmmo()
    {
        int ammoNeededToMaxMag = maxAmmoCount - ammo;

        if (spareAmmo >= ammoNeededToMaxMag)
        {
            ammo += ammoNeededToMaxMag;
            spareAmmo -= ammoNeededToMaxMag;
        }
        else if (spareAmmo > 0)
        {
            ammo += spareAmmo;
            spareAmmo = 0;
        }
        else
        {
            mag--;
            spareAmmo = maxAmmoCount;
            ammo = maxAmmoCount;
        }
    }

    protected override void TurnReloading(bool state) => isReloading = state;

    protected override IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        
        if (bullet != null)
            PhotonNetwork.Destroy(bullet);
    }

    protected override IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(3f);

        ReloadAmmo();
        displayAmmo.SetAmmo(ammo, maxAmmoCount);
        displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);
        TurnReloading(false);
    }
}