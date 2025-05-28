﻿namespace Model;

public class InlineTargetSelector : ITargetSelector
{
    private readonly ShotsGrid _grid;
    private readonly IEnumerable<Square> _squaresHit;
    private readonly int _shipLength;

    private readonly Random _random = new();

    public InlineTargetSelector(ShotsGrid grid, IEnumerable<Square> squaresHit, int shipLength)
    {
        _grid = grid;
        _squaresHit = squaresHit;
        _shipLength = shipLength;
    }

    public Square Next()
    {
        var sorted = _squaresHit.OrderBy(keySelector: s => s.Row + s.Column);
        var directionCandidates = new List<IEnumerable<Square>>();

        if (sorted.First().Row == sorted.Last().Row)
        {
            var left = _grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Leftwards);
            if (left.Any())
            {
                directionCandidates.Add(left);
            }

            var right = _grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Rightwards);
            if (right.Any())
            {
                directionCandidates.Add(right);
            }
        }
        else
        {
            var up = _grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Upwards);
            if (up.Any())
            {
                directionCandidates.Add(up);
            }

            var down = _grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Downwards);
            if (down.Any())
            {
                directionCandidates.Add(down);
            }
        }

        var groupedByLength = directionCandidates.GroupBy(l => l.Count());
        var sortedByLength = groupedByLength.OrderByDescending(g => g.Key);
        var longestDirections = sortedByLength.First();
        var candidates = longestDirections.Count();

        if (candidates == 1)
        {
            return longestDirections.First().First();
        }

        var selectedIndex = _random.Next(candidates);

        return longestDirections.ElementAt(selectedIndex).First();
    }
}