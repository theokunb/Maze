using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider;
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

    private void Start()
    {
        var savedVolume = PlayerPrefs.GetFloat(Constants.Volume, Constants.MaxVolume);
        _slider.value = savedVolume / Constants.MaxVolume;
    }

    public void OnSoud()
    {
        bool isSoundSettingsOpen = _slider.gameObject.activeSelf;

        _slider.gameObject.SetActive(!isSoundSettingsOpen);
    }

    public void OnSoundValueChanged()
    {
        var newVolume = _slider.value * Constants.MaxVolume;

        PlayerPrefs.SetFloat(Constants.Volume, newVolume);
        SoundContainer.Instance.UpdateVolume();
        FocusSoundController.Instance?.SetVolume(newVolume);
    }
}
