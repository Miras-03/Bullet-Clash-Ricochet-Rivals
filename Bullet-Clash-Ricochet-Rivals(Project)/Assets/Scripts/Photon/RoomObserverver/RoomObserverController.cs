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

    public void AddObservers()
    {
        room.AddObserver(playerSetup);
        room.AddObserver(healthManager);
        room.AddObserver(uiRoomObservers);
    }

    private void OnDisable() => room.RemoveObservers();

    public void GetObservers()
    {
        playerSetup = FindObjectOfType<PlayerSetup>();

        AddObservers();
        room.NotifyObservers();
    }
}
