﻿namespace Model;

public abstract class Grid
{
    public readonly int Rows;
    public readonly int Columns;
    protected readonly Square?[,] squares;

    protected Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        squares = new Square[Rows, Columns];

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                squares[r, c] = new Square(r, c);
            }
        }
    }

    public virtual IEnumerable<Square> Squares
    {
        get => squares.Cast<Square>();
    }

    protected abstract bool IsSquareAvailable(int row, int column);

    public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
    {
        return GetVerticalAvailablePlacements(length).Concat(GetHorizontalAvailablePlacements(length));
    }

    private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
    {
        List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

        for (int r = 0; r < Rows; r++)
        {
            var queue = new LimitedQueue<Square>(length);

            for (int c = 0; c < Columns; c++)
            {
                if (IsSquareAvailable(r, c))
                {
                    queue.Enqueue(squares[r, c]!);
                    if (queue.Count() == length)
                    {
                        result.Add(queue.ToArray());
                    }
                }
                else
                {
                    queue.Clear();
                }
            }
        }
        return result;
    }

    private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int length)
    {
        List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

        for (int c = 0; c < Columns; c++)
        {
            var queue = new LimitedQueue<Square>(length);

            for (int r = 0; r < Rows; r++)
            {
                if (IsSquareAvailable(r, c))
                {
                    queue.Enqueue(squares[r, c]!);
                    if (queue.Count() == length)
                    {
                        result.Add(queue.ToArray());
                    }

                }
                else
                {
                    queue.Clear();
                }
            }
        }
        return result;
    }
}