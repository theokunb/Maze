using UnityEngine;

public class SoundContainer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;

    public static SoundContainer Instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateVolume();
    }

    private void Update()
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
        var volume = PlayerPrefs.GetFloat(Constants.Volume, Constants.MaxVolume);

        _audioSource.volume = volume;
    }

    private AudioClip GetRandomClip()
    {
        int rand = Random.Range(0, _clips.Length);

        return _clips[rand];
    }
}
