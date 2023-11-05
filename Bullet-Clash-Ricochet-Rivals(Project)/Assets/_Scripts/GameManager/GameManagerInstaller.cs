using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    public override void InstallBindings() => 
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
}