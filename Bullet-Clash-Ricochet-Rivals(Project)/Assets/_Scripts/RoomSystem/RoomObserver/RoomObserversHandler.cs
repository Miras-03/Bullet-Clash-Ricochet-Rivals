using System;
using Zenject;

public class RoomObserverHandler : IInitializable, IDisposable
{
    private Room room;
    private HealthManager healthManager;
    private UIRoomObserver uiRoomObservers;
    private RoomEnvironment roomEnvironment;

    [Inject]
    public void Constructor(
        Room room, RoomEnvironment roomEnvironment,
        HealthManager healthManager, UIRoomObserver uiRoomObservers) {

        this.room = room;
        this.roomEnvironment = roomEnvironment;
        this.healthManager = healthManager;
        this.uiRoomObservers = uiRoomObservers;
    }

    public void Initialize() => GameManager.OnGameStarted += GetObservers;
    public void Dispose() => GameManager.OnGameStarted -= GetObservers;

    private void GetObservers()
    {
        AddObservers();
        room.NotifyObservers();
    }

    private void AddObservers()
    {
        room.AddObserver(healthManager);
        room.AddObserver(uiRoomObservers);
        room.AddObserver(roomEnvironment);
    }
}