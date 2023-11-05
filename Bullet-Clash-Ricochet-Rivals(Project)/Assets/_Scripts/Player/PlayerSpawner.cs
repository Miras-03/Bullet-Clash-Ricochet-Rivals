using Photon.Pun;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameManager gameManager;

    [SerializeField] private GameObject playerPrefab;

    [Space(20)]
    [Header("SpawnPoints")]
    [SerializeField] private Transform[] spawnPoints;

    private int playerCount;
    private int randomPoint;

    [Inject]
    public void Constructor(GameManager gameManager) => this.gameManager = gameManager;

    private void Start()
    {
        RenderSpawnPoints();
        CheckAndSpawnPlayer();
        gameManager.InvokeGame();
    }

    private void RenderSpawnPoints()
    {
        int firstPlayerSpawnPoint = Random.Range(0, 2);
        int secondPlayerSpawnPoint = Random.Range(3, 6);

        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        randomPoint = playerCount == 1
            ? firstPlayerSpawnPoint
            : secondPlayerSpawnPoint;
    }

    private void CheckAndSpawnPlayer()
    {
        Quaternion playersQuaternion = Quaternion.Euler(0, 180, 0);
        if (IsSecondPlayer())
            playersQuaternion = Quaternion.identity;

        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randomPoint].position, playersQuaternion);

        EnablePrefab(player);
        SetNickname(player);
    }

    private bool IsSecondPlayer() => playerCount > 1;

    private void SpawnPlayer(GameObject player)
    {
        EnablePrefab(player);
        SetNickname(player);
    }

    private void EnablePrefab(GameObject prefab) => prefab.GetComponent<PlayerSetup>().SetupPlayer();

    private void SetNickname(GameObject prefab) =>
        prefab.GetComponent<PhotonView>().RPC(nameof(SetNickname), RpcTarget.AllBuffered);
}