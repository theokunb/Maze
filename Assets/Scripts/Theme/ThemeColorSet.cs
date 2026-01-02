using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSet", menuName = "Theme Color Set/New Color set", order = 54)]
public class ThemeColorSet : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private Color[] _colors;
    [SerializeField] private Material _skyboxMaterial;
    [SerializeField] private string _ruTitle;
    [SerializeField] private string _enTitle;
    [SerializeField] private string _trTitle;

    public int Id => _id;
    public IEnumerable<Color> Colors => _colors;
    public Material SkyboxMaterial => _skyboxMaterial;

    public string GetTitle(string lang)
    {
        return lang switch
        {
            "ru" => _ruTitle,
            "en" => _enTitle,
            "tr" => _trTitle,
            _ => string.Empty,
        };
    }
}