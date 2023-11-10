using UISpace;
using Zenject;

public sealed class UIRoomObserverInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<UIRoomObserver>().FromComponentInHierarchy().AsSingle();
}