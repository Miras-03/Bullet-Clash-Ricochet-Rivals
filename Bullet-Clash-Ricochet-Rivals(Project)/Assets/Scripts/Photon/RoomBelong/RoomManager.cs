using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static event Action OnGameStarted;

    [Space(20)]
    [Header("PlayerProperties")]
    [SerializeField] private GameObject playerPrefab;
    //[SerializeField] private PlayerNickname playerNickname;
    private int playerCount;

    [Space(20)]
    [Header("RoomUIObjects")]
    [SerializeField] private GameObject[] roomEnvironment;


    [SerializeField] private Transform[] spawnPoints;
    private int randomPoint;

    [HideInInspector] public string roomName = "RoomWithoutName";

    private void Start()
    {
        SelectSpawnPoint();
        CheckAndInstantiatePlayer();
    }

    private void SelectSpawnPoint()
    {
        int firstPlayerSpawnPoint = UnityEngine.Random.Range(0, 2);
        int secondPlayerSpawnPoint = UnityEngine.Random.Range(3, 6);

        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        randomPoint = playerCount == 1
            ? firstPlayerSpawnPoint
            : secondPlayerSpawnPoint;
    }

    private void CheckAndInstantiatePlayer()
    {
        Quaternion playersQuaternion = Quaternion.Euler(0, 180, 0);
        if (IsSecondPlayer())
            playersQuaternion = Quaternion.identity;

        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randomPoint].position, playersQuaternion);

        StartCoroutine(WaitForDelay(player));
    }

    private bool IsSecondPlayer() => playerCount > 1;

    private IEnumerator WaitForDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(1);

        EnablePrefab(prefab);
        //playerNickname.SetNickname(prefab);

        foreach (GameObject item in roomEnvironment)
            item.SetActive(false);

        OnGameStarted.Invoke();
    }

    private void EnablePrefab(GameObject prefab) => prefab.GetComponent<PlayerSetup>().IsLocalPlayer();
}