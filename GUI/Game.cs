using GUI;
using Model;
using System;

public enum Turn {
    Player1,
    Player2
}
public class Game
{
    // refactor into getters and setters
    public Turn Turn;

    //public event Action<Square> OnSquareClicked;
    public int GridRows;
    public int GridColumns;

    public Player Player1;
    public Player Player2;
    public Game(int gridRows, int gridColumns)
    {
        GridRows = gridRows;
        GridColumns = gridColumns;

        int[] shipLengths = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
        Player1 = new Player(gridRows, gridColumns, shipLengths);
        Player2 = new Player(gridRows, gridColumns, shipLengths);

        Turn = Turn.Player1; // Start with Player1's turn
    }

    public void SwitchTurns()
    {
        Turn = Turn == Turn.Player1 ? Turn.Player2 : Turn.Player1;
    }
    public HitResult HandleSquareClicked(Square square)
    {
        HitResult result;

        if (Turn == Turn.Player1)
        {
            Console.WriteLine(square.SquareState);
            result = Player2.PlayerFleet.Hit(square.Row, square.Column);
            
        }
        else
        {
            result = Player1.PlayerFleet.Hit(square.Row, square.Column);
        }

        Console.WriteLine(result);
        square.ChangeState(result switch
        {
            HitResult.Missed => SquareState.Missed,
            HitResult.Hit => SquareState.Hit,
            HitResult.Sunken => SquareState.Sunken,
            _ => square.SquareState // Default case, should not happen
        });
        return result;
    }
}