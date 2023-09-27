using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("PlayerProperties")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerTransform;

    private void Start() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("Test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        GameObject prefab = PhotonNetwork.Instantiate(playerPrefab.name, playerTransform.position, Quaternion.identity);
    }
}
