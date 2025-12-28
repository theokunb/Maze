using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private GameObject _toy;
    [SerializeField] private Maze _main;
    [SerializeField] private Maze[] _others;
    [SerializeField] private CellInsideView _template;
    [SerializeField] private Material _materialForStart;
    [SerializeField] private Material _materialForFinish;
    [SerializeField] private Material _materialForHole;

    private int _createdOtherCount = 0;
    private List<CellInsideView> _cellViews = new List<CellInsideView>();

    public event Action FinishAchived;
    public event Action LevelSelected;

    private void OnEnable()
    {
        _main.Created += OnMainCreated;

        foreach (var other in _others)
        {
            other.Created += OnOtherCreated;
        }
    }

    private void OnDisable()
    {
        _main.Created -= OnMainCreated;

        foreach (var other in _others)
        {
            other.Created -= OnOtherCreated;
        }
    }

    private void Start()
    {
        int level = PlayerPrefs.GetInt(Constants.Level, 1);
        level = Math.Clamp(level, 1, 50);

        LoadLevelInfo($"Level {level}");
    }

    private void LoadLevelInfo(string key)
    {
        Addressables.LoadAssetAsync<LevelInfo>(key).Completed += OnLevelInfoLoaded;
    }

    private void OnLevelInfoLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<LevelInfo> obj)
    {
        var levelInfo = obj.Result;

        _main.CreateWith(levelInfo.MainArray);

        int i = 0;
        foreach (var item in levelInfo.OtherArray())
        {
            _others[i].CreateWith(item);

            i++;
        }

        LevelSelected?.Invoke();

        Addressables.Release(obj);
    }
    private void OnMainCreated()
    {
        var startCell = _main.StartCell;
        CellInsideView cellInsideView = Instantiate(_template, startCell.transform);
        cellInsideView.Setup(startCell, _materialForStart);

        CreatePlayer(startCell);
    }

    private void OnOtherCreated()
    {
        _createdOtherCount++;

        if (_createdOtherCount == _others.Length)
        {
            OnAllOtherCreated();
        }
    }

    private IEnumerable<Maze> AllMaze()
    {
        yield return _main;

        foreach(var maze in _others)
        {
            yield return maze;
        }
    }

    private void OnAllOtherCreated()
    {
        foreach(var item in AllMaze())
        {
            if(item.FinishCell != null)
            {
                CellInsideView cellInsideView = Instantiate(_template, item.FinishCell.transform);
                cellInsideView.Setup(item.FinishCell, _materialForFinish);
            }

            MarkAsHole(item);
        }
    }

    private void MarkAsHole(Maze maze)
    {
        foreach(var holeCell in maze.Holes)
        {
            CellInsideView cellInsideView = Instantiate(_template, holeCell.transform);
            cellInsideView.Setup(holeCell, _materialForHole);
            _cellViews.Add(cellInsideView);
        }
    }

    private void CreatePlayer(CellView cellView)
    {
        var player = Instantiate(_playerTemplate, _toy.transform);

        player.Setup(cellView.transform, _main.Size);
        player.ReturnToStart();
    }
}
