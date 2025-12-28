using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Maze _main;
    [SerializeField] private Maze[] _others;


    [SerializeField] private Player _playerTemplate;
    [SerializeField] private CellInsideView _template;
    [SerializeField] private Material _materialForStart;
    [SerializeField] private Material _materialForFinish;
    [SerializeField] private Material _materialForHole;

    private IEnumerable<Maze> AllMaze()
    {
        yield return _main;

        foreach (var maze in _others)
        {
            yield return maze;
        }
    }

    public void Load(LevelInfo levelInfo)
    {
        var size = levelInfo.MainArray.GetLength(0);
        _base.SetSize(size);

        _main.CreateWith(levelInfo.MainArray);

        var startCell = _main.StartCell;
        var cellInsideView = Instantiate(_template, startCell.transform);
        cellInsideView.Setup(startCell, _materialForStart);

        var player = Instantiate(_playerTemplate, transform);
        player.Setup(cellInsideView.transform, _main.Size);
        player.ReturnToStart();

        int i = 0;
        foreach (var other in levelInfo.OtherArray())
        {
            if (_others.Length > i)
            {
                _others[i].CreateWith(other);
            }
            i++;
        }

        foreach (var maze in AllMaze())
        {
            if (maze.FinishCell != null)
            {
                cellInsideView = Instantiate(_template, maze.FinishCell.transform);
                cellInsideView.Setup(maze.FinishCell, _materialForFinish);
            }

            foreach (var holeCell in maze.Holes)
            {
                cellInsideView = Instantiate(_template, holeCell.transform);
                cellInsideView.Setup(holeCell, _materialForHole);
            }
        }
    }
}
