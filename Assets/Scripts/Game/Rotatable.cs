using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private PlayerInput _playerInput;
    private bool _canRotate = false;
    private Vector2 _rotation;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.PlayerMap.Press.performed += OnPressPerformed;
        _playerInput.PlayerMap.Press.canceled += OnPressCanceled;
        _playerInput.PlayerMap.Rotation.performed += OnRotationPerformed;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnPressCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _canRotate = false;
    }

    private void OnPressPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _canRotate = true;
        StartCoroutine(RotateAsync());
    }

    private IEnumerator RotateAsync()
    {
        while (_canRotate)
        {
            _rotation *= _rotationSpeed;
            transform.Rotate(Vector3.down, _rotation.x, Space.World);
            transform.Rotate(Vector3.right, _rotation.y, Space.World);

            yield return null;
        }
    }

    private void OnRotationPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _rotation = obj.ReadValue<Vector2>();
    }
}
