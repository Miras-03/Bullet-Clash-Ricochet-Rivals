using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

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
        foreach (GameObject weaponGameObject in weaponGameObjects)
            weaponGameObject.SetActive(false);

        currentWeaponIndex = (currentWeaponIndex + 1) % weaponGameObjects.Count;
        weaponGameObjects[currentWeaponIndex].SetActive(true);

        playerController.SetWeapon(weapons[currentWeaponIndex]);
    }
}
