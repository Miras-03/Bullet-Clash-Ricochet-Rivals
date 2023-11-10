using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ButtonInstaller : MonoInstaller
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_InputField nicknameInput;

    public override void InstallBindings()
    {
        Container.BindInstance(button).AsSingle();
        Container.BindInstance(nicknameInput).AsSingle();
    }
}