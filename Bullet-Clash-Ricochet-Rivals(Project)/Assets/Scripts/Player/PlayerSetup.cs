using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerCamera;

    public void IsLocalPlayer()
    {
        playerController.enabled = true;
        playerCamera.SetActive(true);
    }
}
