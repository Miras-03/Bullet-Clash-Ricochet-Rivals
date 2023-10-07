using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    [Header("PlayerBelong")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerCamera;

    [Space(20)]
    [Header("PlayerNickname")]
    [SerializeField] private GameObject nameTag; 
    [SerializeField] private TextMeshPro nicknameText;
    private string nickname;

    private void Awake() => RoomManager.OnRoomJoined += EnableNicknameFace;

    public void IsLocalPlayer()
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

    private void EnableNicknameFace() => nameTag.SetActive(true);
}
