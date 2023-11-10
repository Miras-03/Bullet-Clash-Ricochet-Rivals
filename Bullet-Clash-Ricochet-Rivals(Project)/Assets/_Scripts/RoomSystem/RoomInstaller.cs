using Zenject;

namespace RoomSpace
{
    public sealed class RoomInstaller : MonoInstaller
    {
        public override void InstallBindings() => Container.Bind<Room>().AsSingle();
    }
}