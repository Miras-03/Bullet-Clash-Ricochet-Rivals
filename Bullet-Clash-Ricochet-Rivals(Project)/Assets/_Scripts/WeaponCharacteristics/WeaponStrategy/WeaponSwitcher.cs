using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using PlayerSpace;
using Audio;

namespace WeaponSpace
{
    public sealed class WeaponSwitcher : MonoBehaviour
    {
        [Header("OtherScripts")]
        private PhotonView playerPhotonView;

        [Space(20)]
        [Header("Weapons types")]
        [SerializeField] private List<Weapon> weapons;

        [SerializeField] private Bullet.Bullet bullet;
        private PlayerController playerController;
        private PlayerInput playerInput;

        private int currentWeaponIndex = 0;

        private void Awake()
        {
            playerPhotonView = GetComponent<PhotonView>();
            playerController = GetComponent<PlayerController>();

            playerInput = new PlayerInput();
        }

        public void ExecuteWeaponSwitch()
        {
            ResetReload();
            SetNewWeapon();

            playerInput.ChangeFireExpect();

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

            bullet.SetNewWeapon(weapons[currentWeaponIndex]);
            playerController.SetWeapon(weapons[currentWeaponIndex]);
        }

        private void ShowCurrentWeapon(bool showOrNot)
        {
            playerPhotonView.RPC("ExecuteWeaponSwitch", RpcTarget.Others, currentWeaponIndex, showOrNot);
            weapons[currentWeaponIndex].gameObject.SetActive(showOrNot);
        }
        private void SetWeaponIndex() => currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
    }
}