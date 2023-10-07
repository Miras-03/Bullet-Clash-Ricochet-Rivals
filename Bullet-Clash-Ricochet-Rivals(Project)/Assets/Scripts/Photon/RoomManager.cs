using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static event Action OnRoomJoined;

    [SerializeField] private PlayerNickname playerNickname;

    [Space(20)]
    [Header("PlayerProperties")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject healthBar;
    private int playerCount;

    [Space(20)]
    [Header("RoomUIObjects")]
    [SerializeField] private GameObject[] roomEnvironment;


    [SerializeField] private Transform[] spawnPoints;
    private int randomPoint;

    [SerializeField] private TextMeshProUGUI[] ammoTexts;

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

        SelectSpawnPoint();
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        Quaternion playersQuaternion = Quaternion.Euler(0, 180, 0);
        if (playerCount > 1)
            playersQuaternion = Quaternion.identity;

        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randomPoint].position, playersQuaternion);

        StartCoroutine(WaitForDelay(player));
    }

    private void SelectSpawnPoint()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        randomPoint = playerCount == 1
            ? UnityEngine.Random.Range(0, 2)
            : UnityEngine.Random.Range(3, 6);
    }

    private void EnablePrefab(GameObject prefab) => prefab.GetComponent<PlayerSetup>().IsLocalPlayer();
    private void EnableUI(GameObject ui) => ui.SetActive(true);
    private void EnableUIText(TextMeshProUGUI[] texts)
    {
        foreach (TextMeshProUGUI text in texts) 
            text.enabled = true;
    }

    private IEnumerator WaitForDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(1);

        EnablePrefab(prefab);
        EnableUI(healthBar);
        EnableUIText(ammoTexts);
        playerNickname.SetNickname(prefab);

        foreach (GameObject item in roomEnvironment)
            item.SetActive(false);

        OnRoomJoined.Invoke();
    }
}
