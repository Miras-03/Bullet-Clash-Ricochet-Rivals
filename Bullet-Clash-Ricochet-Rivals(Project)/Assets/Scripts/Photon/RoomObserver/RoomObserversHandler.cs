using UnityEngine;
using Zenject;

public class RoomObserverHandler : MonoBehaviour
{
    private Room room;

    [Header("RoomObservers")]
    private PlayerSetup playerSetup;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private UIRoomObservers uiRoomObservers;

    [Inject]
    public void Constructor(Room room) => this.room = room;

    private void OnEnable() => RoomManager.OnGameStarted += GetObservers;

    private void OnDestroy() => room.RemoveObservers();

    private void AddObservers()
    {
        room.AddObserver(playerSetup);
        room.AddObserver(healthManager);
        room.AddObserver(uiRoomObservers);
    }

    private void GetObservers()
    {
        GetReferenceForPlayer();

        AddObservers();
        room.NotifyObservers();
    }

    private void GetReferenceForPlayer() => playerSetup = FindObjectOfType<PlayerSetup>();
}