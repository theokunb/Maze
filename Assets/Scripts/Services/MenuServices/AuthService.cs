using UnityEngine;
using YG;

public class AuthService : MonoBehaviour, IService
{
    [SerializeField] private GetPlayerYG _getPlayerYg;

    private void OnEnable()
    {
        YG2.onGetSDKData += OnGetSdkData;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnGetSdkData;
    }

    public void Auth()
    {
        YG2.OpenAuthDialog();
    }

    public void OnGetSdkData()
    {
        var authWindow = ServiceLocator.Instance.GetService<AuthWindow>();
        if (authWindow != null)
        {
            authWindow.OnClose();
        }

        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if (storage != null)
        {
            storage.Load();
        }
    }

    public void UpdateData()
    {
        var progress = ServiceLocator.Instance.GetService<Progress>();
        if (progress != null)
        {
            progress.UpdateFromStorage();
        }

        var themeService = ServiceLocator.Instance.GetService<ThemeService>();
        if (themeService != null)
        {
            themeService.UpdateFromStorage();
        }

        var soundContainer = ServiceLocator.Instance.GetService<SoundContainer>();
        if (soundContainer != null)
        {
            soundContainer.UpdateVolume();
        }

        if (_getPlayerYg != null)
        {
            _getPlayerYg.gameObject.SetActive(false);
            _getPlayerYg.gameObject.SetActive(true);
        }
    }
}
