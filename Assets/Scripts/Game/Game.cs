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

    private void SetRandomCellInside(out CellView cellView, Maze maze, CellInside cellInside)
    {
        do
        {
            cellView = maze.GetRandomCellView();
        } while (cellView.CellInside != CellInside.None);

        cellView.CellInside = cellInside;
    }

    private void OnMainCreated()
    {
        SetRandomCellInside(out CellView startCell, _main, CellInside.Start);
        CellInsideView cellInsideView = Instantiate(_template, startCell.transform);
        cellInsideView.Setup(startCell, _materialForStart);

        MarkAsHole(_main);
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

    private void OnAllOtherCreated()
    {
        int random = UnityEngine.Random.Range(0, _others.Length);

        SetRandomCellInside(out CellView finishCell, _others[random], CellInside.Finish);
        CellInsideView cellInsideView = Instantiate( _template, finishCell.transform);
        cellInsideView.Setup(finishCell, _materialForFinish);
        cellInsideView.FinishAchived += OnFinish;

        foreach (var other in _others)
        {
            MarkAsHole(other);
        }
    }

    private void MarkAsHole(Maze maze)
    {
        for (int i = 0; i < _holesCountInOneMaze; i++)
        {
            SetRandomCellInside(out CellView holeCell, maze, CellInside.Hole);

            CellInsideView cellInsideView = Instantiate(_template, holeCell.transform);
            cellInsideView.Setup(holeCell, _materialForHole);
            cellInsideView.HoleAchived += OnHole;
            _cellViews.Add(cellInsideView);
        }
    }

    private void CreatePlayer(CellView cellView)
    {
        var player = Instantiate(_playerTemplate, _toy.transform);
        player.Setup(cellView.transform);
        player.ReturnToStart();
        _player = player;
    }

    private void OnFinish(CellInsideView cellInsideView)
    {
        cellInsideView.FinishAchived -= OnFinish;
        FinishAchived?.Invoke();
    }

    private void OnHole()
    {
        _toy.transform.localEulerAngles = Vector3.zero;
        _player.ReturnToStart();
    }
}
