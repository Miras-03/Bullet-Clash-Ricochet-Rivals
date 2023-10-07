using UnityEngine;
using Photon.Pun;
using System;

public class PlayerNickname : MonoBehaviour
{
    [Header("RoomUIObjects")]
    [SerializeField] private GameObject nameScreen;
    [SerializeField] private GameObject[] connectingGameObjects;

    private string nickname = "Unnamed";

    public void ChangeNickname(string nickname) => this.nickname = nickname;

    public void SetNickname(GameObject prefab) => prefab.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);

    public void JoinRoomButtonPressed()
    {
        PhotonNetwork.ConnectUsingSettings();
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
