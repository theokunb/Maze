using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSet", menuName = "Theme Color Set/New Color set", order = 54)]
public class ThemeColorSet : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _localizationKey;
    [SerializeField] private Color[] _colors;
    [SerializeField] private Material _skyboxMaterial;

    public int Id => _id;
    public string LocalizationKey => _localizationKey;
    public IEnumerable<Color> Colors => _colors;
    public Material SkyboxMaterial => _skyboxMaterial;
}
