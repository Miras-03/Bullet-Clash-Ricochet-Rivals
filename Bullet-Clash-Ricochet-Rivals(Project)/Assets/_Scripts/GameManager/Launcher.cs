using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class Launcher : MonoBehaviourPunCallbacks
{
    private SceneManager sceneManager;

    [SerializeField] private string GameVersion = "0.1";

    [SerializeField] private TextMeshProUGUI connectIndicator;

    [Inject]
    public void Construct(SceneManager sceneManager) => this.sceneManager = sceneManager;

    private void Start()
    {
        SetGameVersion();
        PhotonNetwork.ConnectUsingSettings();
        IndicateMessage("Connecting...");
    }

    private void SetGameVersion() => PhotonNetwork.GameVersion = GameVersion;

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        IndicateMessage("Connected!");
        sceneManager.LoadLobbyScene();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        IndicateMessage(cause.ToString());
    }

    private void IndicateMessage(string message) => connectIndicator.text = message;
}