using UnityEngine;
using UnityEngine.Localization.Components;

public class LevelLabel : MonoBehaviour
{
    private LocalizeStringEvent _localizeStringEvent;

    public int CurrentLevel;

    private void Awake()
    {
        _localizeStringEvent = GetComponent<LocalizeStringEvent>();
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        _localizeStringEvent.RefreshString();
    }
}
