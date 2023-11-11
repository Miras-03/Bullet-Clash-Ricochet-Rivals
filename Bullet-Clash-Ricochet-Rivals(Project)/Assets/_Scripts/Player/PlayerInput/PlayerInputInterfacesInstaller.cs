using Zenject;

public sealed class PlayerInputInterfacesInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.BindInterfacesTo<PlayerInput>().AsSingle();
}