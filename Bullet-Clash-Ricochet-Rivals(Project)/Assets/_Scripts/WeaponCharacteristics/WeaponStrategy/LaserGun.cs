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
    public sealed class LaserGun : Weapon
    {
        [SerializeField] private WeaponsData laserGunData;

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
        }

        private void OnEnable()
        {
            SetAmmoIndicators();
            ReloadAmmoIndicator();

            cameraShakeManager.SetMagnitude(nameof(LaserGun));
        }

        private void SetAmmoReferences() => uiAmmo.SetReferences();

        protected override void SetAmmoValues()
        {
            bulletPrefab = laserGunData.bulletPrefab;
            bulletRB = laserGunData.bulletRB;

            ammo = laserGunData.ammo;
            spareAmmo = ammo;
            maxAmmo = ammo;
            mag = laserGunData.mag;

            bulletDestroy.DestroyTime = laserGunData.destroyTime;
        }

        protected override void SetSpeedValue() => fireSpeed = laserGunData.shootSpeed;

        protected override void SetAmmoIndicators() => uiAmmo.SetAmmo(ammo, maxAmmo, Color.red);

        protected override void ReloadAmmoIndicator() => uiAmmo.ReloadAmmoIndicator(mag, ammo, maxAmmo);

        public override void FireAndShake()
        {
            if (ammo > 0)
            {
                AudioSounder.SoundAudio(SoundSingleton.Instance.GetLaserGunSound);

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
            reloadTime = 3f;
            yield return new WaitForSeconds(reloadTime);

            ReloadAmmo();
            TurnReloading(false);

            SetAmmoIndicators();
            ReloadAmmoIndicator();

            AudioSounder.SoundAudio(SoundSingleton.Instance.GetUnlockSound);
        }
    }
}