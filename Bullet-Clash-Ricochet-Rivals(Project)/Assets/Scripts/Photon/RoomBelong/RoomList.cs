using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using ModestTree;

public class RoomList : MonoBehaviourPunCallbacks
{
    public static RoomList Instance;
    [SerializeField] private GameObject roomManagerGameObject;
    [SerializeField] private RoomManager roomManager;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    [Header("UI")]
    [SerializeField] private Transform roomListParent;
    [SerializeField] private GameObject roomListPrefab;
    [SerializeField] private Button createButton;

    private const string RoomWithoutName = nameof(RoomWithoutName);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        roomManager.roomName = RoomWithoutName;
    }

    private IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count <= 0)
            cachedRoomList = roomList;
        else
        {
            foreach (var room in roomList)
            {
                for (var i = 0; i < cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                            newList.Remove(newList[i]);
                        else
                            newList[i] = room;

                        cachedRoomList = newList;
                    }
                }
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform roomItem in roomListParent)
            Destroy(roomItem.gameObject);
        foreach (var room in cachedRoomList)
        {
            GameObject roomItem = Instantiate(roomListPrefab, roomListParent);
            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/16";

            roomItem.GetComponent<RoomItemButton>().roomName = room.Name;
        }
    }

    public void JoinRoomByName(string roomName = RoomWithoutName)
    {
        roomManager.roomName = roomName;
        roomManagerGameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChangeRoomToCreateName(string roomName)
    {
        if (roomName.IsEmpty())
            createButton.interactable = false;
        else
            createButton.interactable = true;

        roomManager.roomName = roomName;
    }
}