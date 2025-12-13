using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class PreloadScene : MonoBehaviour
{
    IEnumerator Start()
    {
        var preloadOperation = LocalizationSettings.InitializationOperation;

        while (!preloadOperation.IsDone)
        {
            yield return null;
        }

        Debug.Log("Локализация загружена");
        SceneManager.LoadScene(Constants.SceneIndex.MenuScene);
    }
}
