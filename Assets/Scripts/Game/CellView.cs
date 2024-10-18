using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private GameObject _upperWall;
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _bottomWall;

    public CellInside CellInside { get; set; }

    public void Setup(Cell cell)
    {
        _upperWall.SetActive(cell.UpperBound);
        _leftWall.SetActive(cell.LeftBound);
        _rightWall.SetActive(cell.RightBound);
        _bottomWall.SetActive(cell.BottomBound);
        CellInside = cell.CellInside;
    }
}

public enum CellInside
{
    None,
    Start,
    Finish,
    Hole
}