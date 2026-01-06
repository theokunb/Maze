using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindow : CustomWindow
{
    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }

    public void OnPlay()
    {
        OnClose();
    }
}
