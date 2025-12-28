using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameService : MonoBehaviour, IService
{
    private ToysService _toysService;

    private void Start()
    {
        _toysService = ServiceLocator.Instance.GetService<ToysService>();

        int level = PlayerPrefs.GetInt(Constants.Level, 1);
        level = Mathf.Clamp(level, 1, 50);

        LoadLevelInfo($"Level {level}");
    }

    private void LoadLevelInfo(string key)
    {
        Addressables.LoadAssetAsync<LevelInfo>(key).Completed += OnLevelInfoLoaded;
    }

    private void OnLevelInfoLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<LevelInfo> obj)
    {
        var levelInfo = obj.Result;

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

        Addressables.Release(obj);
    }

    public void OnFinish()
    {
        int level = PlayerPrefs.GetInt(Constants.Level, 1);
        int maxLevel = PlayerPrefs.GetInt(Constants.MaxLevel, 1);

        if (level < Constants.LevelCount)
        {
            level += 1;
            PlayerPrefs.SetInt(Constants.Level, level);
        }

        if (level > maxLevel)
        {
            PlayerPrefs.SetInt(Constants.MaxLevel, level);
        }

        var gameMenu = ServiceLocator.Instance.GetService<GameMenu>();
        if (gameMenu != null)
        {
            gameMenu.OnFinish();
        }
    }
}
