using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted;

    public void InvokeGame() => OnGameStarted?.Invoke();
}
