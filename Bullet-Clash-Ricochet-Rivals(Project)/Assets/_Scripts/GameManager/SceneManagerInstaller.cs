using Zenject;

public class SceneManagerInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<SceneManager>().AsSingle();
}