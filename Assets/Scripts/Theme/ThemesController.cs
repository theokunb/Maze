using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ThemesController : MonoBehaviour
{
    [SerializeField] private ThemeElement _prefab;
    [SerializeField] private ThemeColorSet[] _colorSets;

    [Header("Materials")]
    [SerializeField] private Material _groundMaterial;
    [SerializeField] private Material _wallMaterial;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _startMaterial;
    [SerializeField] private Material _finishMaterial;
    [SerializeField] private Material _holeMaterial;

    private ToggleGroup _toggleGroup;

    private void Awake()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
    }
    private void OnEnable()
    {
        int setId = PlayerPrefs.GetInt(Constants.ColorSet, 0);

        foreach (var colorSet in _colorSets)
        {
            var themeElement = Instantiate(_prefab, transform);
            themeElement.InitializeColors(colorSet);
            themeElement.ThemeChanged += OnThemeChanged;

            if(themeElement.TryGetComponent(out Toggle toggle))
            {
                toggle.group = _toggleGroup;
                toggle.isOn = setId == colorSet.Id;
            }
        }
    }
    private void OnDisable()
    {
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent(out ThemeElement themeElement))
            {
                themeElement.ThemeChanged -= OnThemeChanged;
                Destroy(child.gameObject);
            }
        }
    }

    private void OnThemeChanged(ThemeColorSet themeColorSet)
    {
        PlayerPrefs.SetInt(Constants.ColorSet, themeColorSet.Id);

        var colors = themeColorSet.Colors.ToArray();
        if(colors.Length != 6)
        {
            Debug.Log("invalid color set");
            return;
        }
        _groundMaterial.color = colors[0];
        _wallMaterial.color = colors[1];
        _playerMaterial.color = colors[2];
        _startMaterial.color = colors[3];
        _finishMaterial.color = colors[4];
        _holeMaterial.color = colors[5];
        RenderSettings.skybox = themeColorSet.SkyboxMaterial;
        DynamicGI.UpdateEnvironment();
    }
}
