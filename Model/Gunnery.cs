﻿using System.Diagnostics;

namespace Model;

public enum ShootingTactics
{
    Random,
    Surrounding,
    Inline
}

public class Gunnery
{
    private readonly List<int> shipLengths = [];
    private readonly ShotsGrid recordGrid;
    private readonly SquareEliminator eliminator = new();
    private Square target;
    private ITargetSelector targetSelector;
    private List<Square> shipSquares = new();

    public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

    public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
    {
        recordGrid = new ShotsGrid(rows, columns);
        this.shipLengths = new List<int>(shipLengths.OrderDescending());

        targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
    }

    public Square Next()
    {
        target = targetSelector.Next();
        return target;
    }

    public void SetTarget(int row, int column)
    {
        target = new Square(row, column);
    }

    public void ProcessHitResult(HitResult hitResult)
    {
        RecordTargetResult(hitResult);

        if (hitResult == HitResult.Hit)
        {
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    ShootingTactics = ShootingTactics.Surrounding;
                    targetSelector = new SurroundingTargetSelector(recordGrid, target, shipLengths[0]);
                    break;
                case ShootingTactics.Surrounding:
                    ShootingTactics = ShootingTactics.Inline;
                    targetSelector = new InlineTargetSelector(recordGrid, shipSquares, shipLengths[0]);
                    break;
            }
        }
        else if (hitResult == HitResult.Sunken)
        {
            if (shipLengths.Count == 0) return;
            ShootingTactics = ShootingTactics.Random;
            targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
        }
    }

    private void RecordTargetResult(HitResult hitResult)
    {
        switch (hitResult)
        {
            case HitResult.Missed:
                target.ChangeState(SquareState.Missed);
                recordGrid.ChangeSquareState(target.Row, target.Column, SquareState.Missed);
                break;
            case HitResult.Hit:
                target.ChangeState(SquareState.Hit);
                recordGrid.ChangeSquareState(target.Row, target.Column, SquareState.Hit);
                shipSquares.Add(target);
                break;
            case HitResult.Sunken:
                MarkShipSunken();
                break;
        }
    }

    private void MarkShipSunken()
    {
        shipSquares.Add(target);

        foreach (var square in shipSquares)
        {
            square.ChangeState(SquareState.Sunken);
        }

        var toEliminate = eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
        foreach (var square in toEliminate)
        {
            recordGrid.ChangeSquareState(square.Row, square.Column, SquareState.Eliminated);
        }
        shipLengths.Remove(shipSquares.Count);
        shipSquares.Clear();
    }
}