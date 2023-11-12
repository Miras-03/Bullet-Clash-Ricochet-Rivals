using Audio;
using Bullet;
using CameraSpace;
using Photon.Pun;
using System.Collections;
using UISpace;
using UnityEngine;
using UnityEngine.UI;

namespace WeaponSpace
{
    public sealed class MachineGun : Weapon
    {
        [SerializeField] private WeaponsData machineGunData;

        [Space(20)]
        [SerializeField] private Transform firePoint;

        [Space(20)]
        [SerializeField] private Animator reloadAnimator;

        private BulletDestroy bulletDestroy;
        private GameObject bulletPrefab;
        private Rigidbody bulletRB;

        private CameraShakeManager cameraShakeManager;
        private UIAmmo uiAmmo;
        private Coroutine reloadCoroutine;

        private Image crosshair;

        private void Awake()
        {
            bulletDestroy = GetComponentInParent<BulletDestroy>();
            cameraShakeManager = GetComponentInParent<CameraShakeManager>();

            uiAmmo = new UIAmmo();
            SetAmmoReferences();
        }

        private void Start()
        {
            SetAmmoValues();
            SetSpeedValue();

            SetAmmoIndicators();
            ReloadAmmoIndicator();
            SetAmmoColor();
        }

        private void OnEnable()
        {
            SetAmmoIndicators();
            ReloadAmmoIndicator();
            SetAmmoColor();

            cameraShakeManager.SetMagnitude(nameof(MachineGun));
        }

        private void SetAmmoReferences() => uiAmmo.SetReferences();

        private void SetAmmoValues()
        {
            bulletPrefab = machineGunData.bulletPrefab;
            bulletRB = machineGunData.bulletRB;

            ammo = machineGunData.ammo;
            spareAmmo = ammo;
            maxAmmo = ammo;
            mag = machineGunData.mag;

            fireSpeed = machineGunData.shootSpeed;

            bulletDestroy.DestroyTime = machineGunData.destroyTime;
        }

        private void SetSpeedValue() => fireSpeed = machineGunData.shootSpeed;

        private void SetAmmoIndicators() => uiAmmo.SetAmmoCount(ammo, maxAmmo);

        private void ReloadAmmoIndicator() => uiAmmo.ReloadAmmoIndicator(mag, ammo, maxAmmo);

        private void SetAmmoColor() => uiAmmo.SetAmmoCircleColor(Color.yellow);

        public override void FireAndShake()
        {
            if (ammo > 0)
            {
                AudioSounder.SoundAudio(SoundSingleton.Instance.GetMachineGunSound);

                ammo--;

                SetAmmoIndicators();
                ReloadAmmoIndicator();

                GameObject bullet = PhotonNetwork.Instantiate(nameof(Bullet), firePoint.position, firePoint.rotation);
                bullet.GetComponent<Bullet.Bullet>().SetNewWeapon(this);
                bulletRB.velocity = bullet.transform.forward * fireSpeed;

                cameraShakeManager.ShakeCamera();

                bulletDestroy.SetAndDestroyBullet(bullet, bulletDestroy.DestroyTime);
            }
            else
            {
                AudioSounder.SoundAudio(SoundSingleton.Instance.GetLuckSound);

                if (mag >= 0)
                    Reload();
                else
                    cameraShakeManager.IsCanceled = !cameraShakeManager.IsCanceled;
            }
        }

        public override void Reload()
        {
            bool hasEnoughAmmoToReload = (spareAmmo > 0 || mag > 0) && (ammo != maxAmmo || ammo == 0);

            if (hasEnoughAmmoToReload)
            {
                TurnReloading(true);
                reloadAnimator.SetTrigger(ReloadWeapon);
                reloadCoroutine = StartCoroutine(WaitForReload());
            }
        }

        public override void TurnReloading(bool state) => isReloading = state;

        private void ReloadAmmo()
        {
            int ammoNeededToMaxMag = maxAmmo - ammo;

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
                spareAmmo = maxAmmo;
                ammo = maxAmmo;
            }
        }

        public override void ResetAnimator() => reloadAnimator.Rebind();

        public override void StopReloadCoroutine()
        {
            if (reloadCoroutine != null)
                StopCoroutine(reloadCoroutine);
        }

        private IEnumerator WaitForReload()
        {
            reloadTime = 3.5f;
            yield return new WaitForSeconds(reloadTime);

            TurnReloading(false);
            ReloadAmmoIndicator();

            ReloadAmmo();
            SetAmmoIndicators();

            AudioSounder.SoundAudio(SoundSingleton.Instance.GetUnlockSound);
        }
    }
}