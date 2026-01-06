using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TutorialWindow : CustomWindow
{
    [SerializeField] private GameObject _playerIndicator;
    [SerializeField] private PointTutorial _pointTutorial;
    [SerializeField] private GameObject _movementTutorial;

    [Header("Tutorial Texts")]
    [SerializeField] TMP_Text _startPointTutorialText;
    [SerializeField] TMP_Text _finishPointTutorialText;
    [SerializeField] TMP_Text _holePointTutorialText;

    private PlayerInput _playerInput;
    private bool _pressed;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _pressed = false;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.PlayerMap.Press.performed += OnPressPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.PlayerMap.Press.performed -= OnPressPerformed;
    }

    public async Task PlayerIndicator(Vector3 screenPos)
    {
        _pressed = false;
        var playerIndicator = Instantiate(_playerIndicator, transform);
        playerIndicator.transform.position = screenPos;

        await WaitClose();
        Destroy(playerIndicator.gameObject);
    }

    public async Task StartPointTutorial()
    {
        _pressed = false;

        var gameWindow = ServiceLocator.Instance.GetService<GameWindow>();
        var targetPosition = gameWindow.StartPointPosition;

        var pointTutorial = Instantiate(_pointTutorial, transform);
        pointTutorial.SetText(_startPointTutorialText.text);
        pointTutorial.transform.position = targetPosition;

        await WaitClose();
        Destroy(pointTutorial.gameObject);
    }

    public async Task FinishPointTutorial()
    {
        _pressed = false;

        var gameWindow = ServiceLocator.Instance.GetService<GameWindow>();
        var targetPosition = gameWindow.FinishPointPosition;

        var pointTutorial = Instantiate(_pointTutorial, transform);
        pointTutorial.SetText(_finishPointTutorialText.text);
        pointTutorial.transform.position = targetPosition;

        await WaitClose();
        Destroy(pointTutorial.gameObject);
    }

    public async Task HolePointTutorial()
    {
        _pressed = false;

        var gameWindow = ServiceLocator.Instance.GetService<GameWindow>();
        var targetPosition = gameWindow.HolePointPosition;

        var pointTutorial = Instantiate(_pointTutorial, transform);
        pointTutorial.SetText(_holePointTutorialText.text);
        pointTutorial.transform.position = targetPosition;

        await WaitClose();
        Destroy(pointTutorial.gameObject);
    }

    public async Task MovementTutorial()
    {
        _pressed = false;

        var movementTutorial = Instantiate(_movementTutorial, transform);

        await WaitClose();
        Destroy(movementTutorial.gameObject);
    }

    private async Task WaitClose()
    {
        do
        {
            await Task.Yield();
        } while (_pressed == false);
    }

    private void OnPressPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _pressed = true;
    }
}
