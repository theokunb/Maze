using UnityEngine;
using YG;
using YG.Insides;

public class Yg2Storage : IStorage
{
    private SaveData _data;

    public SaveData GetData()
    {
        return _data;
    }

    public void Load()
    {
        YGInsides.LoadProgress();

        var maxLevel = YG2.saves.maxLevel;
        if(maxLevel < 1)
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

        _data = new SaveData()
        {
            maxLevel = maxLevel,
            colorSet = YG2.saves.colorSet,
            currentLevel = currentLevel,
            currentVolume = currentVolume,
            zoom = zoom,
        };
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
}

namespace YG
{
    public partial class SavesYG
    {
        public int maxLevel;
        public int currentLevel;
        public int colorSet;
        public float zoom;
        public float currentVolume;
    }
}