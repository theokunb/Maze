using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;

    private SoundContainer _soundContainer;
    private IStorage _storage;
    private bool _status = true;
    private float _currentVolume;

    private void Start()
    {
        _soundContainer = ServiceLocator.Instance.GetService<SoundContainer>();
        _storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = _storage.GetData();
        _currentVolume = data.currentVolume;

        SetImage();
        SetVolume();
    }

    public void OnClick()
    {
        _status = !_status;
        SetImage();
        SetVolume();
    }

    private void SetImage()
    {
        _image.sprite = _status ? _on : _off;
    }

    private void SetVolume()
    {
        float volume;
        if (_status)
        {
            volume = _currentVolume;
        }
        else
        {
            volume = 0f;
        }

        var data = _storage.GetData();
        data.currentVolume = volume;
        _storage.Save();
        
        if (_soundContainer != null)
        {
            _soundContainer.UpdateVolume();
        }
    }
}
