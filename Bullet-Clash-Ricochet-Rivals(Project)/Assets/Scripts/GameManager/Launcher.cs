using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviourPunCallbacks
{
    private SceneManager sceneManager;

    [Header("Important Field")]
    [SerializeField] private string GameVersion = "0.1";

    [Space(20)]
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI connectIndicator;

    [Inject]
    public void Construct(SceneManager sceneManager) => this.sceneManager = sceneManager;

    private void Start()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();
        IndicateMessage("Connecting...");
    }

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