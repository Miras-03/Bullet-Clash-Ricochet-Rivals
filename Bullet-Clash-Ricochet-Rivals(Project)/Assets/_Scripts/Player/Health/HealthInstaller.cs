using Zenject;

namespace HealthSpace
{
    public sealed class HealthInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Health>().AsSingle();
            Container.Bind<HealthManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}