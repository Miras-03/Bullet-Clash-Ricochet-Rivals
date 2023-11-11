using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Audio;
using System;

namespace WeaponSpace
{
    public sealed class WeaponSwitcher : MonoBehaviour
    {
        public static Action<Weapon> OnSwitchWeapon;

        [Header("OtherScripts")]
        private PhotonView playerPhotonView;

        [Space(20)]
        [Header("Weapons types")]
        [SerializeField] private List<Weapon> weapons;

        private int currentWeaponIndex = 0;

        private void Awake() => playerPhotonView = GetComponent<PhotonView>();

        public void ExecuteWeaponSwitch()
        {
            ResetReload();
            SetNewWeapon();
            OnSwitchWeapon.Invoke(weapons[currentWeaponIndex]);

            AudioSounder.SoundAudio(SoundSingleton.Instance.GetUnlockSound);
            ImageSingleton.Instance.SetCrosshair();
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