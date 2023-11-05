using Zenject;

public class HealthInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Health>().AsSingle();
        Container.Bind<HealthManager>().FromComponentInHierarchy().AsSingle();
    }
}