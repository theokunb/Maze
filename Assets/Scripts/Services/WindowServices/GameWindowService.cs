using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameWindowService : WindowService
{
    [SerializeField] private CanvasGroup _canvasGroup;

    protected override void OnEscape(InputAction.CallbackContext obj)
    {
        var last = _windows.LastOrDefault();
        if (last == null)
        {
            return;
        }

        if(last is FinishWindow)
        {
            return;
        }

        if (_windows.Count == 1)
        {
            var pauseWindow = ServiceLocator.Instance.GetService<PauseWindow>();
            if (pauseWindow != null)
            {
                pauseWindow.Popup();
            }
        }

        base.OnEscape(obj);
    }

    public override async void Close(CustomWindow window)
    {
        if (_windows.Count == 2)
        {
            var toyService = ServiceLocator.Instance.GetService<ToysService>();
            if (toyService != null)
            {
                toyService.SetToyActive(true);
            }

            Time.timeScale = 1f;
            _canvasGroup.gameObject.SetActive(false);
            await FadeTask(0.6f, 0f, 0.1f);
        }

        base.Close(window);
    }

    public override async void Popup(CustomWindow window)
    {
        if (_windows.Count == 1)
        {
            var toyService = ServiceLocator.Instance.GetService<ToysService>();
            if (toyService != null)
            {
                toyService.SetToyActive(false);
            }

            Time.timeScale = 0f;
            _canvasGroup.gameObject.SetActive(true);
            await FadeTask(0f, 0.6f, 0.1f);
        }

        base.Popup(window);
    }

    public async Task FadeTask(float from, float to, float delta)
    {
        if(_canvasGroup == null)
        {
            return;
        }

        _canvasGroup.alpha = from;

        while (_canvasGroup.alpha != to)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, to, delta);
            await Task.Yield();
        }
    }
}
