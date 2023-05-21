using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;
    [SerializeField] private AudioSource _audiSource;

    private bool _status = true;

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
        _audiSource.volume = _status ? 1 : 0;
    }
}
