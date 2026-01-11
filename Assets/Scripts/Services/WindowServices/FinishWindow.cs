using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishWindow : CustomWindow
{
    [SerializeField] private GameObject _crownImage;

    private void OnEnable()
    {
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        if(storage == null)
        {
            return;
        }

        var data = storage.GetData();
        _crownImage.SetActive(data.currentLevel == Constants.LevelCount);
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }

    public void OnNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Constants.SceneIndex.GameScene);
    }
}
