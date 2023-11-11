using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ImageInstaller : MonoInstaller
{
    [SerializeField] private Image[] images;

    public override void InstallBindings() => Container.BindInstance(images).AsSingle();
}