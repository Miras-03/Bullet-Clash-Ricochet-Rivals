using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class WeaponSwitcher : MonoBehaviour
{
    [Header("OtherScripts")]
    private PlayerController playerController;
    private PhotonView playerPhotonView;

    [Space(20)]
    [Header("Weapons types")]
    [SerializeField] private List<Weapon> weapons;

    private int currentWeaponIndex = 0;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerPhotonView = GetComponent<PhotonView>();
    }

    public void ExecuteSwitch()
    {
        ResetReload();
        SetNewWeapon();
        UpdateUIIndicators();
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

        playerController.SetWeapon(weapons[currentWeaponIndex]);
    }

    private void SetWeaponIndex() => currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

    private void ShowCurrentWeapon(bool showOrNot)
    {
        playerPhotonView.RPC("ExecuteSwitch", RpcTarget.Others, currentWeaponIndex, showOrNot);
        weapons[currentWeaponIndex].gameObject.SetActive(showOrNot);
    }

    private void UpdateUIIndicators()
    {
        weapons[currentWeaponIndex].SetUIOfAmmo();
        weapons[currentWeaponIndex].ReloadAmmoIndicator();
    }
}
