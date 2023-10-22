using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour, IRoomObserver
{
    [Header("PlayerBelong")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerCamera;

    [Space(20)]
    [Header("PlayerNickname")]
    [SerializeField] private GameObject nameTag; 
    [SerializeField] private TextMeshPro nicknameText;

    [Space(20)]
    [Header("WeaponGameObjects")]
    [SerializeField] private GameObject thirdPersonWeaponHolder;

    private string nickname;

    public void IsLocalPlayer()
    {
        DisableWeapons();
        EnablePlayer();

        GetComponent<Renderer>().enabled = false;
    }

    private void DisableWeapons() => thirdPersonWeaponHolder.SetActive(false);

    private void EnablePlayer()
    {
        playerController.enabled = true;
        playerCamera.SetActive(true);
    }

    [PunRPC]
    public void SetNickname(string nickname)
    {
        this.nickname = nickname;
        nicknameText.text = nickname;
    }

    public void Execute() => nameTag.SetActive(true);
}
