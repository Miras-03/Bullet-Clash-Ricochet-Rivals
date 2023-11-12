using System;
using UISpace;
using Zenject;
using HealthSpace;

namespace RoomSpace
{
    public sealed class RoomObserverHandler : IInitializable, IDisposable
    {
        private Room room;
        private HealthManager healthManager;
        private UIRoomObserver uiRoomObserver;
        private RoomEnvironment roomEnvironment;

        [Inject]
        public void Constructor(
            Room room, RoomEnvironment roomEnvironment,
            HealthManager healthManager) {

            this.room = room;
            this.roomEnvironment = roomEnvironment;
            this.healthManager = healthManager;
        }

        public void Initialize()
        {
            uiRoomObserver = new UIRoomObserver();
            GameManager.OnGameStarted += GetObservers;
        }

        public void Dispose() => GameManager.OnGameStarted -= GetObservers;

        private void GetObservers()
        {
            AddObservers();
            room.NotifyObservers();
        }

        private void AddObservers()
        {
            room.AddObserver(healthManager);
            room.AddObserver(uiRoomObserver);
            room.AddObserver(roomEnvironment);
        }
    }
}