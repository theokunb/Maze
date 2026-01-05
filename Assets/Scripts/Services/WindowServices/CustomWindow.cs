using UnityEngine;

public class CustomWindow : MonoBehaviour, IService
{
    public void Popup()
    {
        var windowService = ServiceLocator.Instance.GetService<WindowService>();
        windowService.Popup(this);
    }

    public void Open()
    {
        var windowService = ServiceLocator.Instance.GetService<WindowService>();
        windowService.Open(this);
    }

    public void OnClose()
    {
        var windowService = ServiceLocator.Instance.GetService<WindowService>();
        if (windowService == null)
        {
            return;
        }

        windowService.Close(this);
    }
}