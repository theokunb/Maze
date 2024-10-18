using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageController : MonoBehaviour
{
    private List<Locale> _locales;
    private int _currentLocaleId;

    private void Awake()
    {
        var locales = LocalizationSettings.AvailableLocales;
        _locales = locales.Locales;
    }

    private void OnEnable()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        _currentLocaleId = _locales.IndexOf(currentLocale);

        LocalizationSettings.SelectedLocale = _locales[_currentLocaleId];
    }

    public void OnLanguageClick()
    {
        _currentLocaleId++;

        if (_currentLocaleId >= _locales.Count)
        {
            _currentLocaleId = 0;
        }

        LocalizationSettings.SelectedLocale = _locales[_currentLocaleId];
    }
}
