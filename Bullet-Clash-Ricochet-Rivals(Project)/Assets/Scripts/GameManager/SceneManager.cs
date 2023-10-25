public class SceneManager
{
    private const string Lobby = nameof(Lobby);
    private const string GameProcess = nameof(GameProcess);

    public void LoadLobbyScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(Lobby);

    public void LoadGameScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(GameProcess);
}