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

    public string? Winner;

    //public event Action<Square> OnSquareClicked;
    public int GridRows;
    public int GridColumns;

    public int shipCount;

    public Player Player1;
    public Player Player2;
    public Game(int gridRows, int gridColumns)
    {
        GridRows = gridRows;
        GridColumns = gridColumns;

        shipCount = 10;
        Winner = null;
        int[] shipLengths = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
        Player1 = new Player(gridRows, gridColumns, shipLengths);
        Player2 = new Player(gridRows, gridColumns, shipLengths);

        Turn = Turn.Player1; // Start with Player1's turn
    }

    public void SwitchTurns()
    {
        if (Player1.ShipsSunken == shipCount)
        {
            Winner = "Player1";
            return;
        }
        else if (Player2.ShipsSunken == shipCount)
        {
            Winner = "Player2";
            return;
        }
        Turn = Turn == Turn.Player1 ? Turn.Player2 : Turn.Player1;

        if (Turn == Turn.Player2)
        {
            var target = Player2.Gunnery.Next();
            Console.WriteLine($"Player2's turn. Targeting square at Row: {target.Row}, Column: {target.Column}");
            var result = HandleSquareClicked(target);
            Console.WriteLine($"Result of Player2's attack: {result}");
            Player2.Gunnery.ProcessHitResult(result);
        }
    }
    public HitResult HandleSquareClicked(Square square)
    {
        Player player = Turn == Turn.Player1 ? Player1 : Player2;
        HitResult result;
        
        result = player.PlayerFleet.Hit(square.Row, square.Column);
        if (result == HitResult.Sunken)
        {
            player.ShipsSunken++;
        }
        SwitchTurns();
        return result;
    }
}