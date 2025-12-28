using UnityEngine;

public class GameEntryPoint : EntryPoint
{
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private GameService _gameService;
    [SerializeField] private ToysService _toysService;
    [SerializeField] private ThemeService _themeService;

    private void Awake()
    {
        ServiceLocator.Instance.Register(_gameMenu);
        ServiceLocator.Instance.Register(_cameraFollow);
        ServiceLocator.Instance.Register(_gameService);
        ServiceLocator.Instance.Register(_toysService);
        ServiceLocator.Instance.Register(_themeService);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.Unregister<GameMenu>();
        ServiceLocator.Instance.Unregister<CameraFollow>();
        ServiceLocator.Instance.Unregister<GameService>();
        ServiceLocator.Instance.Unregister<ToysService>();
        ServiceLocator.Instance.Unregister<ThemeService>();
    }
}
