using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ButtonInstaller : MonoInstaller
{
    [SerializeField] private Button button;

    public override void InstallBindings() => Container.BindInstance(button).AsSingle();
}