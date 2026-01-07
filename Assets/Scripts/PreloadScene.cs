using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadScene : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }
}
