using UnityEngine;

public class GameWindow : CustomWindow
{
    [SerializeField] private LevelLabel _levelLabel;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private Transform _holePoint;
    
    public Vector3 StartPointPosition => _startPoint.position;
    public Vector3 FinishPointPosition => _finishPoint.position;
    public Vector3 HolePointPosition => _holePoint.position;

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
        
    }
}
