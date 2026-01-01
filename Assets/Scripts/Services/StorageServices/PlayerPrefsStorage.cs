using UnityEngine;

public class PlayerPrefsStorage : IStorage
{
    private SaveData _data;

    public PlayerPrefsStorage()
    {
    }

    public SaveData GetData() => _data;

    public void Load()
    {
        _data = new SaveData();

        _data.maxLevel = PlayerPrefs.GetInt(Constants.MaxLevel, 1);
        _data.currentLevel = PlayerPrefs.GetInt(Constants.Level, 1);
        _data.colorSet = PlayerPrefs.GetInt(Constants.ColorSet, 0);
        _data.zoom = PlayerPrefs.GetFloat(Constants.Zoom, Constants.DefaultZoom);
        _data.currentVolume = PlayerPrefs.GetFloat(Constants.Volume, Constants.MaxVolume);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(Constants.MaxLevel, _data.maxLevel);
        PlayerPrefs.SetInt(Constants.Level, _data.currentLevel);
        PlayerPrefs.SetInt(Constants.ColorSet, _data.colorSet);
        PlayerPrefs.SetFloat(Constants.Zoom, _data.zoom);
        PlayerPrefs.SetFloat(Constants.Volume, _data.currentVolume);
    }
}
