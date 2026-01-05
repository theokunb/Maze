using UnityEngine;
using YG.LanguageLegacy;

public class LevelLabel : MonoBehaviour
{
    [SerializeField] private LangYGAdditionalText _additinalText;

    public int CurrentLevel;

    public void SetLevel(int level)
    {
        CurrentLevel = level;

        if(_additinalText == null)
        {
            return;
        }

        _additinalText.additionalText = $" {CurrentLevel}";
    }
}
