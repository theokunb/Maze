using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PreloadScene : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }
}
