using UISpace;
using UnityEngine;

namespace WeaponSpace
{
    public abstract class Weapon : MonoBehaviour
    {
        protected int ammo;
        protected int mag;
        protected int spareAmmo;
        protected int maxAmmo;
        protected int fireSpeed;

        protected float reloadTime = 3f;
        protected bool isReloading = false;
        protected const string ReloadWeapon = nameof(ReloadWeapon);

        public abstract void FireAndShake();
        public abstract void Reload();
        public abstract void TurnReloading(bool state);
        public abstract void StopReloadCoroutine();
        public abstract void ResetAnimator();

        public int ShotSpeed { get => fireSpeed; }
        public bool IsReloading { get => isReloading; }
    }
}