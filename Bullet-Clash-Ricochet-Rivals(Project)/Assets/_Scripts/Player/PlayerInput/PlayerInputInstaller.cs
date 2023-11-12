using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<PlayerInput>().AsSingle();
}