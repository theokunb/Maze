using UnityEngine;

public class GameEntryPoint : EntryPoint
{
    [Header("Windows")]
    [SerializeField] private WindowService _windowService;
    [SerializeField] private GameWindow _gameWindow;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private FinishWindow _finishWindow;
    [SerializeField] private TutorialWindow _tutorialWindow;

    [Space(10)]
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private GameService _gameService;
    [SerializeField] private ToysService _toysService;
    [SerializeField] private ThemeService _themeService;
    [SerializeField] private TutorialService _tutorialService;
    [SerializeField] private Yg2Storage _yg2Storage;

    private void Awake()
    {
        ServiceLocator.Instance.Register(_windowService);
        ServiceLocator.Instance.Register(_gameWindow);
        ServiceLocator.Instance.Register(_pauseWindow);
        ServiceLocator.Instance.Register(_finishWindow);

        ServiceLocator.Instance.Register(_cameraFollow);
        ServiceLocator.Instance.Register(_gameService);
        ServiceLocator.Instance.Register(_toysService);
        ServiceLocator.Instance.Register(_themeService);
        ServiceLocator.Instance.Register(_tutorialService);
        ServiceLocator.Instance.Register(_tutorialWindow);

        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if (storage == null)
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
        ServiceLocator.Instance.Unregister<WindowService>();
        ServiceLocator.Instance.Unregister<GameWindow>();
        ServiceLocator.Instance.Unregister<PauseWindow>();
        ServiceLocator.Instance.Unregister<FinishWindow>();

        ServiceLocator.Instance.Unregister<CameraFollow>();
        ServiceLocator.Instance.Unregister<GameService>();
        ServiceLocator.Instance.Unregister<ToysService>();
        ServiceLocator.Instance.Unregister<ThemeService>();
        ServiceLocator.Instance.Unregister<TutorialService>();
        ServiceLocator.Instance.Unregister<TutorialWindow>();
    }

    private void RegisterDontDestroy<T>(T service) where T : MonoBehaviour, IService
    {
        var registerd = ServiceLocator.Instance.GetService<T>();
        if (registerd == null)
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
