using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static event Action OnRoomConnected;

    private Room room;

    [Space(20)]
    [Header("PlayerProperties")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerNickname playerNickname;
    private DisplayAmmo displayAmmo;
    private int playerCount;

    [Space(20)]
    [Header("RoomUIObjects")]
    [SerializeField] private GameObject[] roomEnvironment;


    [SerializeField] private Transform[] spawnPoints;
    private int randomPoint;

    [HideInInspector] public string roomName = "RoomWithoutName";

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

    private IEnumerator WaitForDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(1);

        EnablePrefab(prefab);
        playerNickname.SetNickname(prefab);

        foreach (GameObject item in roomEnvironment)
            item.SetActive(false);

        OnRoomConnected.Invoke();
    }
}