using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenuService : MonoBehaviour, IService
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _authWindow;

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
        _menuWindow.SetActive(false);
        _settingsWindow.SetActive(true);
    }

    public void OnSettingsBack()
    {
        _settingsWindow.SetActive(false);
        _menuWindow.SetActive(true);

        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if (storage != null)
        {
            storage.Save();
        }
    }

    public void OnAuth()
    {
        var auth = YG2.player.auth;
        if(auth == true)
        {
            return;
        }

        _authWindow.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
