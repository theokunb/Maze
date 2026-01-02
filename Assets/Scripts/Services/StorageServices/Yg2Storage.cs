using UnityEngine;
using YG;
using YG.Insides;

public class Yg2Storage : MonoBehaviour, IStorage
{
    private SaveData _data;

    private void Awake()
    {
        _data = new SaveData();
    }

    private void OnEnable()
    {
        YG2.onGetSDKData += OnGetSdkData;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= OnGetSdkData;
    }

    public SaveData GetData() => _data;

    public void Load()
    {
        YGInsides.LoadProgress();
    }

    public void Save()
    {
        YG2.saves.maxLevel = _data.maxLevel;
        YG2.saves.currentLevel = _data.currentLevel;
        YG2.saves.colorSet = _data.colorSet;
        YG2.saves.zoom = _data.zoom;
        YG2.saves.currentVolume = _data.currentVolume;

        YG2.SaveProgress();
    }

    private void OnGetSdkData()
    {
        var maxLevel = YG2.saves.maxLevel;
        if (maxLevel < 1)
        {
            maxLevel = 1;
        }

        var currentLevel = YG2.saves.currentLevel;
        if (currentLevel < 1)
        {
            currentLevel = 1;
        }

        var currentVolume = YG2.saves.currentVolume;
        currentVolume = Mathf.Clamp(currentVolume, 0f, Constants.MaxVolume);

        var zoom = YG2.saves.zoom;
        if (zoom == 0f)
        {
            zoom = Constants.DefaultZoom;
        }

        _data.maxLevel = maxLevel;
        _data.colorSet = YG2.saves.colorSet;
        _data.currentLevel = currentLevel;
        _data.currentVolume = currentVolume;
        _data.zoom = zoom;

        var authService = ServiceLocator.Instance.GetService<AuthService>();
        if (authService != null)
        {
            authService.UpdateData();
        }
    }
}