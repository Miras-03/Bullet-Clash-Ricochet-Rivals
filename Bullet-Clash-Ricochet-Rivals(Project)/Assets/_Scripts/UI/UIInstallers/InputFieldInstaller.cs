using TMPro;
using UnityEngine;
using Zenject;

public sealed class InputFieldInstaller : MonoInstaller
{
    [SerializeField] private TMP_InputField inputField;

    public override void InstallBindings() => Container.BindInstance(inputField).AsSingle();
}