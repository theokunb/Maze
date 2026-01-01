using UnityEngine;
using YG.LanguageLegacy;

public class LevelLabel : MonoBehaviour
{
    private LangYGAdditionalText _additinalText;

    public int CurrentLevel;

    private void Awake()
    {
        _additinalText = GetComponent<LangYGAdditionalText>();
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        _additinalText.additionalText = $" {CurrentLevel}";
    }
}
