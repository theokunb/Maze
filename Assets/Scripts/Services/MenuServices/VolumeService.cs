using UnityEngine;
using UnityEngine.UI;

public class VolumeService : MonoBehaviour, IService
{
    [SerializeField] private Slider _slider;

    private SoundContainer _soundContainer;
    private FocusSoundController _focusSoundController;
    private IStorage _storage;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSoundValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSoundValueChanged);
    }

    private void Start()
    {
        _storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = _storage.GetData();
        _slider.value = data.currentVolume / Constants.MaxVolume;
        _soundContainer = ServiceLocator.Instance.GetService<SoundContainer>();
        _focusSoundController = ServiceLocator.Instance.GetService<FocusSoundController>();
    }

    public void OnSoud()
    {
        bool isSoundSettingsOpen = _slider.gameObject.activeSelf;

        _slider.gameObject.SetActive(!isSoundSettingsOpen);
    }
    private void OnSoundValueChanged(float value)
    {
        var data = _storage.GetData();
        var newVolume = value * Constants.MaxVolume;
        data.currentVolume = newVolume;
        
        if(_soundContainer != null)
        {
            _soundContainer.UpdateVolume();
        }
        if (_focusSoundController != null)
        {
            _focusSoundController.SetVolume(newVolume);
        }
    }
}
