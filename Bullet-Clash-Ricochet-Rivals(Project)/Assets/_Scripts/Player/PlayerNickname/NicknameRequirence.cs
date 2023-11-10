using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class NicknameRequirence : MonoBehaviour
{
    private TMP_InputField pickName;
    private Button quickButton;

    [Inject]
    public void Contructor(TMP_InputField inputfield, Button button)
    {
        pickName = inputfield;
        quickButton = button;
    }

    public void CheckOrIncludeInteractable() => quickButton.interactable = !pickName.text.IsEmpty();
}
