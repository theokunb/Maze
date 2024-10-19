using UnityEngine;

public class FocusSoundController : MonoBehaviour
{
    public static FocusSoundController Instance;

    private float _currentVolume;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _currentVolume = PlayerPrefs.GetFloat(Constants.Volume, Constants.MaxVolume);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    private void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        PlayerPrefs.SetFloat(Constants.ApplicationFocusVolume, silence ? 0 : _currentVolume);
        SoundContainer.Instance?.UpdateFromApplicationFocusVolume();
    }

    public void SetVolume(float volume)
    {
        _currentVolume = volume;
    }
}
