using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
