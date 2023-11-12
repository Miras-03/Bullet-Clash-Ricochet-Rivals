using Photon.Pun;
using PlayerSpace;
using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private PlayerController playerController;
    private PhotonView photonView;
    private UIGameOver uiGameOver;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        photonView = GetComponent<PhotonView>();
        uiGameOver = new UIGameOver();
    }

    [PunRPC]
    public void PerformGameOver()
    {
        Destroy(playerController);

        if (photonView.IsMine)
            uiGameOver.SetColorAndMessage(Color.red, "You lose!");
        else
            uiGameOver.SetColorAndMessage(Color.green, "You won!!!");

        StartCoroutine(WaitAndMoveToLauncher());
    }

    private IEnumerator WaitAndMoveToLauncher()
    {
        yield return new WaitForSeconds(3);

        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;

        PhotonNetwork.LoadLevel(0);

        EnableCursor();
    }

    private void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}