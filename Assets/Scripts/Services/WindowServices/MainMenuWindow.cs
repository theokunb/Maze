using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : CustomWindow
{
    public void OnPlay()
    {
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if (storage != null)
        {
            storage.Save();
        }

        SceneManager.LoadSceneAsync(Constants.SceneIndex.GameScene);
    }

    public void OnSettings()
    {
        var settingsWindow = ServiceLocator.Instance.GetService<SettingsWindow>();
        if (settingsWindow == null)
        {
            return;
        }

        settingsWindow.Open();
    }

    public void OnAuth()
    {
        var authWindow = ServiceLocator.Instance.GetService<AuthWindow>();
        if (authWindow == null)
        {
            return;
        }

        authWindow.Popup();
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
