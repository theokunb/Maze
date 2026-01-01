using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThemeService : MonoBehaviour, IService
{
    [Header("Themes")]
    [SerializeField] private ThemeColorSet[] _colorSets;

    [Header("Materials")]
    [SerializeField] private Material _groundMaterial;
    [SerializeField] private Material _wallMaterial;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _startMaterial;
    [SerializeField] private Material _finishMaterial;
    [SerializeField] private Material _holeMaterial;

    private IStorage _storage;

    public IEnumerable<ThemeColorSet> ColorSets { get => _colorSets; }
    public int SetId { get; private set; }

    private void Start()
    {
        _storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = _storage.GetData();
        var setId = data.colorSet;
        SetId = setId;

        ApplySet(SetId);
    }

    public void ApplySet(int setId)
    {
        if(!_colorSets.Any(x => x.Id == setId))
        {
            return;
        }

        var themeColorSet = _colorSets.FirstOrDefault(x => x.Id == setId);
        if(themeColorSet == null)
        {
            return;
        }

        var colors = themeColorSet.Colors.ToArray();
        if (colors.Length != 6)
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

        var data = _storage.GetData();
        if(setId != data.colorSet)
        {
            data.colorSet = setId;
            _storage.Save();
        }
    }
}
