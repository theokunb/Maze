using System;
using UnityEngine;

public class Cell : ICloneable
{
    public Cell() 
    {
        Plurality = new Plurality();
    }

    private Cell(Cell cell)
    {
        LeftBound = cell.LeftBound;
        RightBound = cell.RightBound;
        UpperBound = cell.UpperBound;
        BottomBound = cell.BottomBound;
        SetPlurality(cell.Plurality);
    }

    public bool LeftBound { get; set; }
    public bool UpperBound { get; set; }
    public bool RightBound { get; set; }
    public bool BottomBound { get; set; }
    public Plurality Plurality { get; private set; }
    public CellInside CellInside { get; set; }

    public void InitPlurality()
    {
        Plurality.Init();
        Plurality.Add(this);
    }

    public void MergeWith(Cell other)
    {
        other.JoinToPlurality(Plurality);
    }

    public bool IsUniqueWithoutBottomBound()
    {
        return Plurality.IsUniuqeWithoutBottomBound();
    }

    public bool IsUniqueInPlurality()
    {
        return Plurality.IsUnique(this);
    }

    public void SetPlurality(Plurality plurality)
    {
        Plurality = plurality;
    }

    public void LeavePlurality()
    {
        LeftPlurality();
    }

    private void JoinToPlurality(Plurality plurality)
    {
        LeftPlurality();
        plurality.Add(this);
    }

    private void LeftPlurality()
    {
        Plurality.Remove(this);
    }

    public object Clone()
    {
        return new Cell(this);
    }
}
