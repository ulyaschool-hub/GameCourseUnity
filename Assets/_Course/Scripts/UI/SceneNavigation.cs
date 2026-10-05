using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    [SerializeField] private string gameplayScene = "Lab08_UI";
    [SerializeField] private string mainMenuScene = "Lab08_MainMenu";

    public void StartGame()
    {
        Load(gameplayScene);
    }

    public void RestartCurrent()
    {
        Load(SceneManager.GetActiveScene().name);
    }

    public void OpenMainMenu()
    {
        Load(mainMenuScene);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private static void Load(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}