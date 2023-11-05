using Zenject;

public class UIRoomObserverInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<UIRoomObserver>().FromComponentInHierarchy().AsSingle();
}