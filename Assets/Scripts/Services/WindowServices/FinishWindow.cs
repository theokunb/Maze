using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishWindow : CustomWindow
{
    [SerializeField] private GameObject _crownImage;

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

    public void SetCrowImageActivity(bool activity)
    {
        if(_crownImage == null)
        {
            Debug.Log("crow image is null");
            return;
        }

        _crownImage.SetActive(activity);
    }
}
