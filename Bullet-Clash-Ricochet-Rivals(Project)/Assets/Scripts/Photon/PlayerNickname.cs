using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;
using ModestTree;

public class PlayerNickname : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private RoomManager roomManager;

    [Space(20)]
    [Header("RoomUIObjects")]
    [SerializeField] private GameObject nameScreen;
    [SerializeField] private GameObject[] connectingGameObjects;
    [SerializeField] private Button joinRoomButton;

    private string nickname = "Unnamed";

    public void ChangeNickname(string nickname)
    {
        this.nickname = nickname;
        if (!nickname.IsEmpty())
            joinRoomButton.interactable = true;
        else
            joinRoomButton.interactable = false;
    }

    public void SetNickname(GameObject prefab) => prefab.GetComponent<PhotonView>().RPC(nameof(SetNickname), RpcTarget.AllBuffered, nickname);

    public void JoinRoomButtonPressed()
    {
        PhotonNetwork.JoinOrCreateRoom(roomManager.roomName, null, null);
        SwapGameObjects();
    }

    private void SwapGameObjects()
    {
        gameObject.SetActive(false);
        nameScreen.SetActive(false);

        foreach (GameObject connectingGameObject in connectingGameObjects) 
            connectingGameObject.SetActive(true);
    }
}
