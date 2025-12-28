using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour, IService
{
    [SerializeField] private Rotatable _toy;
    [SerializeField] private Game _game;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private LevelLabel _levelLabel;

    private async Task FadeTask(float from, float to, float delta)
    {
        _canvasGroup.alpha = from;

        while (_canvasGroup.alpha != to)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, to, delta);
            await Task.Yield();
        }
    }

    public void OnPause()
    {
        OpenMenu(_menu);
    }

    public void OnPlay()
    {
        CloseMenu(_menu);
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

    private async void CloseMenu(GameObject menu)
    {
        Time.timeScale = 1;
        _toy.enabled = true;
        await FadeTask(0.4f, 0, 0.01f);
        _canvasGroup.gameObject.SetActive(false);
        menu.SetActive(false);
    }

    private async void OpenMenu(GameObject menu)
    {
        Time.timeScale = 0;
        _toy.enabled = false;
        _canvasGroup.gameObject.SetActive(true);
        await FadeTask(0, 0.4f, 0.01f);
        menu.SetActive(true);
    }

    public void OnFinish()
    {
        OpenMenu(_finishMenu);
    }

    public void SetLevelLabel()
    {
        int level = PlayerPrefs.GetInt(Constants.Level, 1);

        _levelLabel.SetLevel(level);
    }
}
