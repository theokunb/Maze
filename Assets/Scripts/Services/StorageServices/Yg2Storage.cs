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
        _data = new SaveData()
        {
            maxLevel = YG2.saves.maxLevel,
            colorSet = YG2.saves.colorSet,
            currentLevel = YG2.saves.currentLevel,
            currentVolume = YG2.saves.currentVolume,
            zoom = YG2.saves.zoom,
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