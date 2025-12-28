using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuService : MonoBehaviour, IService
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _settingsWindow;

    public void OnPlay()
    {
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
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
