using Zenject;

public class RoomInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Room>().AsSingle();
}