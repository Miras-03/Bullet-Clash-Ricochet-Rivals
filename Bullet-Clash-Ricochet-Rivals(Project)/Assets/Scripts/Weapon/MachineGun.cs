using Photon.Pun;
using System.Collections;
using UnityEngine;

public class MachineGun : Weapon
{
    [Header("WeaponDates")]
    [SerializeField] private WeaponsData machineGunData;

    public override async void FireAsync()
    {
        if (ammo > 0)
        {
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetGunSound);

            ammo--;
            SetUIOfAmmo();
            ReloadAmmoIndicator();

            GameObject bullet = PhotonNetwork.Instantiate(Bullet, firePoint.position, firePoint.rotation);
            bulletRB.velocity = bullet.transform.forward * shootSpeed;

            if (!isCanceled)
                await cameraShake.Shake(shakeDuration, magnitudeOfCamera);

            bulletDestroy.DestroyBullet(bullet, destroyTime);
        }
        else
        {
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetLuckSound);
            if (mag >= 0)
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
            reloadAnimator.SetTrigger(ReloadWeapon);
            reloadCoroutine = StartCoroutine(WaitForReload());
        }
    }

    public override void TurnReloading(bool state) => isReloading = state;

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

    protected override void SetValues()
    {
        bulletPrefab = machineGunData.bulletPrefab;
        bulletRB = machineGunData.bulletRB;

        ammo = machineGunData.ammo;
        spareAmmo = ammo;
        maxAmmoCount = ammo;
        mag = machineGunData.mag;

        shootSpeed = machineGunData.shootSpeed;

        magnitudeOfCamera = machineGunData.magnitudeOfCamera;

        destroyTime = machineGunData.destroyTime;
    }

    public override void SetUIOfAmmo() => displayAmmo.SetAmmo(ammo, maxAmmoCount);
    public override void ReloadAmmoIndicator() => displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);

    public override void StopReloadCoroutine()
    {
        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);
    }

    public override void ResetAnimator() => reloadAnimator.Rebind();

    public override IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(reloadTime);

        ReloadAmmo();
        SetUIOfAmmo();
        ReloadAmmoIndicator();
        TurnReloading(false);
    }
}