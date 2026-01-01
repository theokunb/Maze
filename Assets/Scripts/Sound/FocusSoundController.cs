using UnityEngine;

public class FocusSoundController : MonoBehaviour, IService
{
    private float _currentVolume;

    private void Start()
    {
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = storage.GetData();
        _currentVolume = data.currentVolume;
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
        float applicationFocusVolume;
        if (silence)
        {
            applicationFocusVolume = 0f;
        }
        else
        {
            applicationFocusVolume = _currentVolume;
        }

        var soundContainer = ServiceLocator.Instance.GetService<SoundContainer>();
        if(soundContainer != null)
        {
            soundContainer.UpdateFromApplicationFocusVolume(applicationFocusVolume);
        }
    }

    public void SetVolume(float volume)
    {
        _currentVolume = volume;
    }
}
