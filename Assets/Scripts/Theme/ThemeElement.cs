using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ThemeElement : MonoBehaviour
{
    [SerializeField] private GameObject _colorElementPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _text;

    private RectTransform _rectTransform;
    private ThemeColorSet _colorSet;
    private string _localizationKey = null;

    public event Action<ThemeColorSet> ThemeChanged;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetWidth();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(_localizationKey))
        {
            return;
        }

        var table = LocalizationSettings.StringDatabase.GetTable("localizationTable");
        if (table != null)
        {
            var entry = table.GetEntry(_localizationKey);
            if (entry != null)
            {
                string localizedText = entry.GetLocalizedString();
                _text.text = localizedText;
            }
        }
    }

    public void InitializeColors(ThemeColorSet themeColorSet)
    {
        _colorSet = themeColorSet;
        _localizationKey = themeColorSet.LocalizationKey;

        foreach (var color in themeColorSet.Colors)
        {
            var colorElement = Instantiate(_colorElementPrefab, _container);
            var image = colorElement.GetComponent<Image>();
            image.color = color;
        }
    }

    private void SetWidth()
    {
        var height = Mathf.Abs(_rectTransform.localPosition.x) / 2;
        Vector2 newSize = _rectTransform.sizeDelta;
        newSize.y = height;
        _rectTransform.sizeDelta = newSize;
    }

    public void OnValueChanged(bool value)
    {
        if (value == false)
        {
            return;
        }

        var themeService = ServiceLocator.Instance.GetService<ThemeService>();
        if(themeService == null)
        {
            return;
        }

        themeService.ApplySet(_colorSet.Id);
    }
}
