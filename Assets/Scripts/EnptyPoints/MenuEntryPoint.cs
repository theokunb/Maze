using UnityEngine;

public class MenuEntryPoint : EntryPoint
{
    [SerializeField] private MainMenuService _mainMenu;
    [SerializeField] private VolumeService _volumeService;
    [SerializeField] private ThemeService _themeService;
    [SerializeField] private Progress _progress;
    [SerializeField] private AuthService _authService;

    [Space(10)]
    [SerializeField] private SoundContainer _soundContainer;
    [SerializeField] private FocusSoundController _focusSoundController;
    [SerializeField] private Yg2Storage _yg2Storage;

    [Space(20)]
    [SerializeField] TestService _testService;

    private void Awake()
    {
        ServiceLocator.Instance.Register(_testService);
        ServiceLocator.Instance.Register(_authService);
        ServiceLocator.Instance.Register(_progress);
        ServiceLocator.Instance.Register(_mainMenu);
        ServiceLocator.Instance.Register(_volumeService);
        ServiceLocator.Instance.Register(_themeService);

        RegisterDontDestroy(_soundContainer);
        RegisterDontDestroy(_focusSoundController);
        
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if(storage == null)
        {
            storage = _yg2Storage;
            ServiceLocator.Instance.Register(storage);
            DontDestroyOnLoad(_yg2Storage);
        }
        else
        {
            Destroy(_yg2Storage.gameObject);
        }
        storage.Load();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.Unregister<TestService>();
        ServiceLocator.Instance.Unregister<AuthService>();
        ServiceLocator.Instance.Unregister<Progress>();
        ServiceLocator.Instance.Unregister<ThemeService>();
        ServiceLocator.Instance.Unregister<MainMenuService>();
        ServiceLocator.Instance.Unregister<VolumeService>();
    }

    private void RegisterDontDestroy<T>(T service) where T : MonoBehaviour, IService
    {
        var registerd = ServiceLocator.Instance.GetService<T>();
        if(registerd == null)
        {
            ServiceLocator.Instance.Register(service);
            DontDestroyOnLoad(service);
        }
        else
        {
            Destroy(service.gameObject);
        }
    }
}
