using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;

    private bool _status = true;
    private float _currentVolume;

    private void Awake()
    {
        var volume = PlayerPrefs.GetFloat(Constants.Volume, Constants.MaxVolume);
        _currentVolume = volume;
    }

    private void Start()
    {
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
        PlayerPrefs.SetFloat(Constants.Volume, _status ? _currentVolume : 0);
        SoundContainer.Instance?.UpdateVolume();
    }
}
