using Zenject;

public class RoomObserverInterfacesInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.BindInterfacesTo<RoomObserverHandler>().AsSingle();
}