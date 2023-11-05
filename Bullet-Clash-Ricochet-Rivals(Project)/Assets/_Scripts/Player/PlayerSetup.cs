using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    [Header("PlayerBelong")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerCamera;

    [Space(20)]
    [Header("UI")]
    [SerializeField] private TextMeshPro nicknameText;

    [Space(20)]
    [Header("WeaponGameObjects")]
    [SerializeField] private GameObject thirdPersonWeaponHolder;

    public void SetupPlayer()
    {
        DisableWeapons();
        EnablePlayer();
        HidePersonBody();
    }

    private void DisableWeapons() => thirdPersonWeaponHolder.SetActive(false);

    private void EnablePlayer()
    {
        playerController.enabled = true;
        playerCamera.SetActive(true);
    }

    private void HidePersonBody() => GetComponent<Renderer>().enabled = false;

    [PunRPC]
    public void SetNickname() => nicknameText.text = PhotonNetwork.NickName;
}