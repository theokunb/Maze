using UnityEngine;

public class SoundContainer : MonoBehaviour, IService
{
    [SerializeField] private AudioClip[] _clips;

    private AudioSource _audioSource;
    private IStorage _storage;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _storage = ServiceLocator.Instance.GetService<IStorage>();
        UpdateVolume();
    }

    private void LateUpdate()
    {
        if (!_audioSource.isPlaying)
        {
            var clip = GetRandomClip();
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    public void UpdateVolume()
    {
        var data = _storage.GetData();
        var volume = data.currentVolume;

        _audioSource.volume = volume;
    }

    public void UpdateFromApplicationFocusVolume(float applicationFocusVolume)
    {
        _audioSource.volume = applicationFocusVolume;
    }

    private AudioClip GetRandomClip()
    {
        int rand = Random.Range(0, _clips.Length);

        return _clips[rand];
    }
}
