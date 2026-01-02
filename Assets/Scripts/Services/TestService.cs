using TMPro;
using UnityEngine;

public class TestService : MonoBehaviour, IService
{
    [SerializeField] private TMP_Text _text;

    public TMP_Text Text => _text;
}
