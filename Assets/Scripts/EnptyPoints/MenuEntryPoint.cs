using UnityEngine;

public class MenuEntryPoint : EntryPoint
{
    [SerializeField] private MainMenuService _mainMenu;
    [SerializeField] private VolumeService _volumeService;
    [SerializeField] private ThemeService _themeService;

    [SerializeField] private SoundContainer _soundContainer;
    [SerializeField] private FocusSoundController _focusSoundController;

    private void Awake()
    {
        ServiceLocator.Instance.Register(_mainMenu);
        ServiceLocator.Instance.Register(_volumeService);
        ServiceLocator.Instance.Register(_themeService);

        RegisterDontDestroy(_soundContainer);
        RegisterDontDestroy(_focusSoundController);

        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if(storage == null)
        {
            storage = new Yg2Storage();
            ServiceLocator.Instance.Register(storage);
        }
        storage.Load();
    }

    private void OnDestroy()
    {
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
