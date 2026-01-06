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
}
