using Zenject;

public class RoomEnvironmentInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<RoomEnvironment>().FromComponentInHierarchy().AsSingle();
}