using Zenject;

public class MovementInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Movement>().AsSingle();
}