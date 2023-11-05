using UnityEngine;

public class RoomEnvironment : MonoBehaviour, IRoomObserver
{
    [SerializeField] private Camera roomCamera;

    public void Execute() => roomCamera.enabled = false;
}