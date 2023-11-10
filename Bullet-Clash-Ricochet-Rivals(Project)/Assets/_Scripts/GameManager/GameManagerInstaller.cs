using Zenject;

public sealed class GameManagerInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
}