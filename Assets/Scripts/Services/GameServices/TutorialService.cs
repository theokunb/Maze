using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TutorialService : MonoBehaviour, IService
{
    private bool _active;
    private IList<ITutorialStep> _tutorialSteps;

    private void Start()
    {
        _tutorialSteps = new List<ITutorialStep>();
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = storage.GetData();
        var currentLevel = data.currentLevel;

        _active = currentLevel == 0;
    }

    public void AddTutorialStep(ITutorialStep step) => _tutorialSteps.Add(step);

    public async Task Run()
    {
        if (_active == false)
        {
            return;
        }

        var tutorialWindow = ServiceLocator.Instance.GetService<TutorialWindow>();
        if (tutorialWindow != null)
        {
            tutorialWindow.Popup();
        }

        foreach (var step in _tutorialSteps)
        {
            await step.Invoke();
        }

        if (tutorialWindow != null)
        {
            tutorialWindow.OnClose();
        }
    }
}

public interface ITutorialStep
{
    Task Invoke();
}

public class ShowPlayerTutorial : ITutorialStep
{
    private readonly Transform _playerTransform;

    public ShowPlayerTutorial(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public async Task Invoke()
    {
        var camera = Camera.main;
        var screenPos = camera.WorldToScreenPoint(_playerTransform.position);

        var tutorialWindow = ServiceLocator.Instance.GetService<TutorialWindow>();
        await tutorialWindow.PlayerIndicator(screenPos);
    }
}

public class StartPointTutorial : ITutorialStep
{
    public async Task Invoke()
    {
        var tutorialWindow = ServiceLocator.Instance.GetService<TutorialWindow>();
        await tutorialWindow.StartPointTutorial();
    }
}

public class HolePointTutorial : ITutorialStep
{
    public async Task Invoke()
    {
        var tutorialWindow = ServiceLocator.Instance.GetService<TutorialWindow>();
        await tutorialWindow.HolePointTutorial();
    }
}

public class FinishPointTutorial : ITutorialStep
{
    public async Task Invoke()
    {
        var tutorialWindow = ServiceLocator.Instance.GetService<TutorialWindow>();
        await tutorialWindow.FinishPointTutorial();
    }
}

public class MovementTutorial : ITutorialStep
{
    public async Task Invoke()
    {
        var tutorialWindow = ServiceLocator.Instance.GetService<TutorialWindow>();
        await tutorialWindow.MovementTutorial();
    }
}