using UnityEngine;
using UnityEngine.UI;

public class ThemesController : MonoBehaviour
{
    [SerializeField] private ThemeElement _prefab;

    private ToggleGroup _toggleGroup;

    private void Awake()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
    }

    private void Start()
    {
        var themeService = ServiceLocator.Instance.GetService<ThemeService>();
        if (themeService == null)
        {
            return;
        }

        var setId = themeService.SetId;

        foreach (var colorSet in themeService.ColorSets)
        {
            var themeElement = Instantiate(_prefab, transform);
            themeElement.InitializeColors(colorSet);

            if (themeElement.TryGetComponent(out Toggle toggle))
            {
                toggle.group = _toggleGroup;
                toggle.isOn = setId == colorSet.Id;
            }
        }
    }
}
