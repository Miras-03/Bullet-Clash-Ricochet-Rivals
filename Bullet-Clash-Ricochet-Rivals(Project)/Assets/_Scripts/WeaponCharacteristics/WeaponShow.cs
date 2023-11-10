using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public sealed class WeaponShow : MonoBehaviour
    {
        [Header("WeaponShowGameObjects")]
        [SerializeField] private List<GameObject> weaponShowGameObjects;

        [PunRPC]
        public void ExecuteWeaponSwitch(int weaponIndex, bool state) =>
            weaponShowGameObjects[weaponIndex].SetActive(state);
    }
}