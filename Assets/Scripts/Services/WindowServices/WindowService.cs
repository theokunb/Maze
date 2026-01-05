using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowService : MonoBehaviour, IService
{
    [SerializeField] private CustomWindow _defaultWindow;

    private PlayerInput _playerInput;
    protected List<CustomWindow> _windows;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _windows = new List<CustomWindow>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.PlayerMap.Escape.performed += OnEscape;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.PlayerMap.Escape.performed -= OnEscape;
    }

    private void Start()
    {
        if(_defaultWindow == null)
        {
            return;
        }

        _defaultWindow.Open();
    }

    public virtual void Open(CustomWindow window)
    {
        foreach (var windowElement in _windows)
        {
            windowElement.gameObject.SetActive(false);
        }

        _windows.Add(window);
        window.gameObject.SetActive(true);
    }

    public virtual void Popup(CustomWindow window)
    {
        _windows.Add(window);
        window.gameObject.SetActive(true);
    }

    public virtual void Close(CustomWindow window)
    {
        _windows.Remove(window);
        window.gameObject.SetActive(false);

        var last = _windows.LastOrDefault();
        if (last != null)
        {
            last.gameObject.SetActive(true);
        }
    }

    protected virtual void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_windows.Count <= 1)
        {
            return;
        }

        var last = _windows.Last();
        Close(last);
    }
}