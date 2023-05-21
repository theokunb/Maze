using System.Collections.Generic;
using System.Linq;

public class Plurality
{
    private static int PluralityId = 0;
    private List<Cell> _cells;

    public Plurality() 
    {
        _cells = new List<Cell>();
        Id = 0;
    }

    public int Id { get; private set; }

    public void Init()
    {
        if(Id != 0)
        {
            return;
        }

        PluralityId++;
        Id = PluralityId;
    }

    public bool IsUnique(Cell cell)
    {
        if (_cells.Contains(cell) == false)
        {
            throw new System.Exception($"Plurality has not {cell}");
        }
        else
        {
            return _cells.Count == 1;
        }
    }

    public bool IsUniuqeWithoutBottomBound()
    {
        return _cells.Where(element => element.BottomBound == false).Count() <= 1;
    }

    public void Remove(Cell cell)
    {
        _cells.Remove(cell);
        cell.SetPlurality(new Plurality());
    }

    public void Add(Cell cell)
    {
        _cells.Add(cell);
        cell.SetPlurality(this);
    }
}
