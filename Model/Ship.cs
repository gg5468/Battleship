﻿﻿namespace Model;

public enum HitResult
{
    Missed,
    Hit,
    Sunken
}

public class Ship
{
    public readonly IEnumerable<Square> Squares;

    public Ship(IEnumerable<Square> squares)
    {
        Squares = squares;
    }

    public bool Contains(int row, int column)
    {
        return Squares.FirstOrDefault(s => s.Row == row && s.Column == column) is not null;
    }

    public HitResult Hit(int row, int column)
    {
        var square = Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column);
        if (square == null)
        {
            return HitResult.Missed;
        }

        square.Hit();

        if (Squares.All(sq => sq.IsHit))
        {
            foreach (var sq in Squares)
            {
                sq.ChangeState(SquareState.Sunken);
            }

            return HitResult.Sunken;
        }
        return HitResult.Hit;
    }
}