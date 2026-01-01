using System;

public interface IStorage : IService
{
    public void Save();
    public void Load();

    public SaveData GetData();
}

[Serializable]
public class SaveData
{
    public int maxLevel;
    public int currentLevel;
    public int colorSet;
    public float zoom;
    public float currentVolume;
}
