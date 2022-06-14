﻿using GenerateLib.Viewable;

namespace GenerateLib.Components;

public class Cell : Component, IViewable
{
    // HardNumber = had a value above 0 from the start. Unchangeable. 
    public bool HardNumber { get; }
    public List<Row> Rows { get; }
    public List<Square> Squares { get; }
    public List<Column> Columns { get; }
    public bool IsClone { get; set; }

    public Cell(int value, int x, int y, bool hardNumber)
    {
        Rows = new List<Row>();
        Squares = new List<Square>();
        Columns = new List<Column>();
        HardNumber = hardNumber;
        Value = value;
        X = x;
        Y = y;
    }

    public override void Add(Component c)
    {
        throw new NotImplementedException();
    }

    public override bool IsComposite()
    {
        return false;
    }

    public override bool HasEmptyCell()
    {
        return Value == 0;
    }

    public override Cell? FindEmptyCell()
    {
        return Value == 0 ? this : null;
    }
    
    public bool IsValueDuplicateInRows(int number)
    {
        return Rows.Any(row => row.HasDuplicateCellValue(this, number));
    }

    public bool IsValueDuplicateInColumns(int number)
    {
        return Columns.Any(col => col.HasDuplicateCellValue(this, number));
    }

    public bool IsValueDuplicateInSquares(int number)
    {
        return Squares.Any(square => square.HasDuplicateCellValue(this, number));
    }
}