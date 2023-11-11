using TMPro;
using UnityEngine;
using Zenject;

public class TextMeshProInstallers : MonoInstaller
{
    [SerializeField] private TextMeshProUGUI[] text;

    public override void InstallBindings() => Container.BindInstance(text).AsSingle();
}