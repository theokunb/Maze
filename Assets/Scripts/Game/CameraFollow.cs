using UnityEngine;

public class CameraFollow : MonoBehaviour, IService
{
    [SerializeField] private float _minZoom = 0.9f;
    [SerializeField] private float _maxZoom = 2.1f;

    private Vector3 _startPosition;
    private float _zoom;
    private PlayerInput _playerInput;
    private Base _base;

    private void Awake()
    {
        _zoom = PlayerPrefs.GetFloat(Constants.Zoom, 1.7f);
        _startPosition = transform.position;
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.PlayerMap.MouseWheel.performed += OnMouseWheel;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.PlayerMap.MouseWheel.performed -= OnMouseWheel;
    }

    public void OnBaseSizeChanged()
    {
        if (_base == null)
        {
            return;
        }

        transform.position = _startPosition - _base.Size * _zoom * transform.forward;
    }

    private void OnMouseWheel(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();
        _zoom = (-1) * value * 0.05f + _zoom;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        Debug.Log(_zoom);

        OnBaseSizeChanged();
        PlayerPrefs.SetFloat(Constants.Zoom, _zoom);
    }

    public void SetBase(Base mazeBase) => _base = mazeBase;
}
