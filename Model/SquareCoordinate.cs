﻿namespace Model;

public struct SquareCoordinate
{
    public readonly int Row;
    public readonly int Column;

    public SquareCoordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }
}