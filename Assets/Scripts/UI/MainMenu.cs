using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void OnPlay()
    {
        SceneManager.LoadSceneAsync(1);
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
