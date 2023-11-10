using System;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted;

    public void InvokeGame() => OnGameStarted?.Invoke();
}
