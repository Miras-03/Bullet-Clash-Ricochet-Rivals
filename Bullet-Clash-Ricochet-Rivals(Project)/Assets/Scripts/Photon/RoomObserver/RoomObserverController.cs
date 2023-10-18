using UnityEngine;
using Zenject;

public class RoomObserverController : MonoBehaviour
{
    private Room room;

    [Header("RoomObservers")]
    private PlayerSetup playerSetup;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private UIRoomObservers uiRoomObservers;

    [Inject]
    public void Construct(Room room) => this.room = room;

    private void OnEnable() => RoomManager.OnRoomConnected += GetObservers;
    private void OnDestroy() => RoomManager.OnRoomConnected -= GetObservers;

    public void AddObservers()
    {
        room.AddObserver(playerSetup);
        room.AddObserver(healthManager);
        room.AddObserver(uiRoomObservers);
    }

    private void OnDisable() => room.RemoveObservers();

    private void GetObservers()
    {
        playerSetup = FindObjectOfType<PlayerSetup>();

        AddObservers();
        room.NotifyObservers();
    }
}
