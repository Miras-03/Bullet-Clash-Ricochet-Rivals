using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class WeaponSwitcher : MonoBehaviour
{
    [Header("OtherScripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private DisplayAmmo displayAmmo;

    [Space(20)]
    [Header("WeaponGameObjects")]
    [SerializeField] private List<GameObject> weaponGameObjects;

    [Space(20)]
    [Header("WeaponScripts")]
    [SerializeField] private List<Weapon> weapons;

    private int currentWeaponIndex = 0;

    [PunRPC]
    public void SwitchWeaponGameObject()
    {
        SetOffCurrentProcess();
        SetNewWeapon();
        UpdateUIIndicators();
    }

    private void SetOffCurrentProcess()
    {
        weaponGameObjects[currentWeaponIndex].SetActive(false);
        //weapons[currentWeaponIndex].TurnReloading(false);
        //weapons[currentWeaponIndex].StopReloadCoroutine();
    }

    private void SetNewWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponGameObjects.Count;

        weaponGameObjects[currentWeaponIndex].SetActive(true);
        playerController.SetWeapon(weapons[currentWeaponIndex]);
        weapons[currentWeaponIndex].ResetAnimator();
    }

    private void UpdateUIIndicators()
    {
        weapons[currentWeaponIndex].SetUIOfAmmo();
        weapons[currentWeaponIndex].ReloadAmmoIndicator();
    }
}
