using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishWindow : CustomWindow
{
    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }

    public void OnNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Constants.SceneIndex.GameScene);
    }
}
