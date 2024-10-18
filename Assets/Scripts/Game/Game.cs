using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private GameObject _toy;
    [SerializeField] private int _holesCountInOneMaze;
    [SerializeField] private Maze _main;
    [SerializeField] private Maze[] _others;
    [SerializeField] private CellInsideView _template;
    [SerializeField] private Material _materialForStart;
    [SerializeField] private Material _materialForFinish;
    [SerializeField] private Material _materialForHole;

    private Player _player;
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

        foreach(var cellInsideView in _cellViews)
        {
            cellInsideView.HoleAchived -= OnHole;
        }
    }

    private void Start()
    {
        int level;
        LevelInfo levelInfo = null;

        while(levelInfo == null)
        {
            level = PlayerPrefs.GetInt(Constants.Level, 1);
            if(level < 1)
            {
                level = 1;
            }

            levelInfo = Resources.Load($"{Constants.Level} {level}") as LevelInfo;

            if(levelInfo == null)
            {
                PlayerPrefs.SetInt(Constants.Level, level - 1);
            }
            else
            {
                PlayerPrefs.SetInt(Constants.Level, level);
            }
        }

        _main.CreateWith(levelInfo.MainArray);

        int i = 0;
        foreach (var item in levelInfo.OtherArray())
        {
            _others[i].CreateWith(item);

            i++;
        }

        LevelSelected?.Invoke();
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
                cellInsideView.FinishAchived += OnFinish;
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
            cellInsideView.HoleAchived += OnHole;
            _cellViews.Add(cellInsideView);
        }
    }

    private void CreatePlayer(CellView cellView)
    {
        var player = Instantiate(_playerTemplate, _toy.transform);

        player.Setup(cellView.transform, _main.Size);
        player.ReturnToStart();
        _player = player;
    }

    private void OnFinish(CellInsideView cellInsideView)
    {
        cellInsideView.FinishAchived -= OnFinish;
        FinishAchived?.Invoke();

        int level = PlayerPrefs.GetInt(Constants.Level, 1);
        PlayerPrefs.SetInt(Constants.Level, level + 1);
    }

    private void OnHole()
    {
        _toy.transform.localEulerAngles = Vector3.zero;
        _player.ReturnToStart();
    }
}
