using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static event Action OnRoomJoined;
    [SerializeField] private HealthManager healthManager;

    [Header("PlayerProperties")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject healthBar;

    [SerializeField] private Transform playerTransform;

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
        GameObject prefab = PhotonNetwork.Instantiate(playerPrefab.name, playerTransform.position, Quaternion.identity);
        StartCoroutine(WaitForDelay(prefab));
    }

    private void EnablePrefab(GameObject prefab) => prefab.GetComponent<PlayerSetup>().IsLocalPlayer();
    private void EnableUI(GameObject ui) => ui.SetActive(true);

    private IEnumerator WaitForDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(1);
        EnablePrefab(prefab);
        EnableUI(healthBar);
        OnRoomJoined.Invoke();
    }
}
