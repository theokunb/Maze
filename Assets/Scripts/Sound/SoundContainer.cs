using System.Linq;
using UnityEngine;

public class SoundContainer : MonoBehaviour, IService
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;

    [Space(10)]
    [SerializeField] private AudioSource[] _childAudioSources;

    private void Start()
    {
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
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = storage.GetData();
        var volume = data.currentVolume;

        _audioSource.volume = volume;

        foreach (var child in _childAudioSources)
        {
            child.volume = volume;
        }
    }

    public void UpdateFromApplicationFocusVolume(float applicationFocusVolume)
    {
        _audioSource.volume = applicationFocusVolume;

        foreach (var child in _childAudioSources)
        {
            child.volume = applicationFocusVolume;
        }
    }

    public AudioSource GetFreeChildAudioSource()
    {
        return _childAudioSources.FirstOrDefault(x => x.isPlaying == false);
    }

    private AudioClip GetRandomClip()
    {
        int rand = Random.Range(0, _clips.Length);

        return _clips[rand];
    }
}
