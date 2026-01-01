using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.LanguageLegacy;

public class ThemeElement : MonoBehaviour
{
    [SerializeField] private GameObject _colorElementPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _text;

    private RectTransform _rectTransform;
    private ThemeColorSet _colorSet;

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
        if(_colorSet == null)
        {
            return;
        }

        var lang = YG2.lang;
        _text.text = _colorSet.GetTitle(lang);
    }

    public void InitializeColors(ThemeColorSet themeColorSet)
    {
        _colorSet = themeColorSet;

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
        if (themeService == null)
        {
            return;
        }

        themeService.ApplySet(_colorSet.Id);
    }
}
