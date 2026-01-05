using UnityEngine;

public class GameWindow : CustomWindow
{
    [SerializeField] private LevelLabel _levelLabel;

    public void OnPause()
    {
        var pauseWindow = ServiceLocator.Instance.GetService<PauseWindow>();
        if(pauseWindow == null)
        {
            return;
        }
        pauseWindow.Popup();
    }

    public void SetLevelLabel()
    {
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = storage.GetData();
        _levelLabel.SetLevel(data.currentLevel);
    }

    public void OnFinish()
    {
        var finishWindow = ServiceLocator.Instance.GetService<FinishWindow>();
        if(finishWindow == null)
        {
            return;
        }

        finishWindow.Popup();
    }
}
