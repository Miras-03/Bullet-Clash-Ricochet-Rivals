using System;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private SceneManager sceneManager;

    [Inject]
    public void Constructor(SceneManager sceneManager) => this.sceneManager = sceneManager;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    public void QuickStart() => PhotonNetwork.JoinLobby();

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

    public override void OnJoinRandomFailed(short returnCode, string message) => CreateQuickStartRoom();

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

    public override void OnJoinedRoom() => sceneManager.LoadGameScene();
}