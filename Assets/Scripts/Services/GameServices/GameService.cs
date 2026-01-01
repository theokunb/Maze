using System.Linq;
using UnityEngine;

public class GameService : MonoBehaviour, IService
{
    private ToysService _toysService;
    private IStorage _storage;

    private void Start()
    {
        _toysService = ServiceLocator.Instance.GetService<ToysService>();
        _storage = ServiceLocator.Instance.GetService<IStorage>();

        var data = _storage.GetData();
        int level = data.currentLevel;
        level = Mathf.Clamp(level, 1, Constants.LevelCount);

        var levelInfo = Resources.Load<LevelInfo>($"Level {level}");
        if(levelInfo == null)
        {
            return;
        }

        OnLevelInfoLoaded(levelInfo);
    }

    private void OnLevelInfoLoaded(LevelInfo levelInfo)
    {
        var sideCount = levelInfo.OtherArray().Count() + 1;
        var toyPrefab = _toysService.GetToyPrefab(sideCount);

        var toy = Instantiate(toyPrefab, Vector3.zero, Quaternion.identity);
        toy.Load(levelInfo);
        _toysService.SetToy(toy);

        var gameMenu = ServiceLocator.Instance.GetService<GameMenu>();
        if (gameMenu != null)
        {
            gameMenu.SetLevelLabel();
        }
    }

    public void OnFinish()
    {
        var data = _storage.GetData();

        int level = data.currentLevel;
        int maxLevel = data.maxLevel;

        if (level < Constants.LevelCount)
        {
            level += 1;
            data.currentLevel = level;
        }

        if (level > maxLevel)
        {
            data.maxLevel = level;
        }
        _storage.Save();

        var gameMenu = ServiceLocator.Instance.GetService<GameMenu>();
        if (gameMenu != null)
        {
            gameMenu.OnFinish();
        }
    }
}
