using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Audio;
using System;

namespace WeaponSpace
{
    public sealed class WeaponSwitcher : MonoBehaviour
    {
        [Header("Weapons types")]
        [SerializeField] private List<Weapon> weapons;

        private PhotonView playerPhotonView;

        public static Action<Weapon> OnSwitchWeapon;

        private int currentWeaponIndex = 0;

        private void Awake() => playerPhotonView = GetComponent<PhotonView>();

        public void ExecuteWeaponSwitch()
        {
            ResetReload();
            SetNewWeapon();
            OnSwitchWeapon.Invoke(weapons[currentWeaponIndex]);

            AudioSounder.SoundAudio(SoundSingleton.Instance.GetUnlockSound);
            Crosshair.Instance.SwitchCrosshair();
        }

        private void ResetReload()
        {
            weapons[currentWeaponIndex].ResetAnimator();
            weapons[currentWeaponIndex].TurnReloading(false);
            weapons[currentWeaponIndex].StopReloadCoroutine();
        }

        private void SetNewWeapon()
        {
            ShowCurrentWeapon(false);
            SetWeaponIndex();
            ShowCurrentWeapon(true);
        }

        private void ShowCurrentWeapon(bool showOrNot)
        {
            playerPhotonView.RPC("ExecuteWeaponSwitch", RpcTarget.Others, currentWeaponIndex, showOrNot);
            weapons[currentWeaponIndex].gameObject.SetActive(showOrNot);
        }
        private void SetWeaponIndex() => currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
    }
}