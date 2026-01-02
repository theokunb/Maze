using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour, IService
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Button _increaseButton;
    [SerializeField] private Button _decreaseButton;

    private int _maxLevel;
    private int _currentLevel;

    private void OnEnable()
    {
        _increaseButton.onClick.AddListener(OnIncrease);
        _decreaseButton.onClick.AddListener(OnDecrease);
    }

    private void OnDisable()
    {
        _increaseButton.onClick.RemoveListener(OnIncrease);
        _decreaseButton.onClick.RemoveListener(OnDecrease);
    }

    private void Start()
    {
        UpdateFromStorage();
    }

    private void OnIncrease()
    {
        SetCurrentLevel(_currentLevel + 1);
    }

    private void OnDecrease()
    {
        SetCurrentLevel(_currentLevel - 1);
    }

    private void SetCurrentLevel(int value)
    {
        if(value > _maxLevel)
        {
            return;
        }

        if(value < 1)
        {
            return;
        }

        _currentLevel = value;
        _levelText.text = value.ToString();

        var storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = storage.GetData();
        data.currentLevel = _currentLevel;
    }

    public void UpdateFromStorage()
    {
        var storage = ServiceLocator.Instance.GetService<IStorage>();
        var data = storage.GetData();
        _maxLevel = data.maxLevel;

        SetCurrentLevel(data.currentLevel);
    }
}
