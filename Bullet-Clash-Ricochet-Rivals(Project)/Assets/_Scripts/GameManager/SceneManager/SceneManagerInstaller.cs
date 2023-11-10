using Zenject;

public sealed class SceneManagerInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<SceneManager>().AsSingle();
}