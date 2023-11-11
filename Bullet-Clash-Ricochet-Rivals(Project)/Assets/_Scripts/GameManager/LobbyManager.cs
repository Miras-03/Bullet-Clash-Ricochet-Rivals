using System;
using Nickname;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private NicknameChanger playerNickname;
    private SceneManager sceneManager;

    private TextMeshProUGUI loadMessage;
    private TextMeshProUGUI[] uiTexts;
    private Image[] uiImages;

    [Inject]
    public void Constructor(SceneManager sceneManager, TextMeshProUGUI[] texts, Image[] images) 
    {
        this.sceneManager = sceneManager;
        loadMessage = texts[0];
        uiTexts = texts;
        uiImages = images;
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        QuickStart();
    }

    public void QuickStart()
    {
        playerNickname.SetNickname();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (!PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CountOfRooms == 0)
                CreateQuickStartRoom();
            else
                PhotonNetwork.JoinRandomRoom();
        }
    }

    private void CreateQuickStartRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true,
        };

        PhotonNetwork.CreateRoom(Guid.NewGuid().ToString(), roomOptions);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) => CreateQuickStartRoom();

    public override void OnJoinedRoom()
    {
        DisableImages();
        DisableTexts();

        loadMessage.enabled = true;
        loadMessage.text = "Loading room...";

        sceneManager.LoadGameScene();
    }

    private void DisableImages()
    {
        foreach (Image image in uiImages)
            image.enabled = false;
    }
    private void DisableTexts()
    {
        foreach (TextMeshProUGUI text in uiTexts)
            text.enabled = false;
    }
}