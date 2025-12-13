using UnityEngine;
using static UnityEditor.Progress;

public class DemoMaze : MonoBehaviour
{
    [SerializeField] private Maze _maze;
    [SerializeField] private LevelInfo _demoLevel;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private CellInsideView _template;
    [SerializeField] private Material _materialForStart;
    [SerializeField] private Material _materialForFinish;
    [SerializeField] private Material _materialForHole;

    private void Awake()
    {
        _maze.CreateWith(_demoLevel.MainArray);

        var startCell = _maze.StartCell;
        CellInsideView cellInsideView = Instantiate(_template, startCell.transform);
        cellInsideView.Setup(startCell, _materialForStart);

        var player = Instantiate(_playerTemplate, gameObject.transform);

        player.Setup(startCell.transform, _maze.Size);
        player.ReturnToStart();

        if (_maze.FinishCell != null)
        {
            cellInsideView = Instantiate(_template, _maze.FinishCell.transform);
            cellInsideView.Setup(_maze.FinishCell, _materialForFinish);
        }

        foreach (var holeCell in _maze.Holes)
        {
            cellInsideView = Instantiate(_template, holeCell.transform);
            cellInsideView.Setup(holeCell, _materialForHole);
        }
    }
}
