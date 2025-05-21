namespace Model;

public class Fleet
{
    private List<Ship> ships = new List<Ship>();

    public IEnumerable<Ship> Ships { get { return ships; } }

    public void CreateShip(IEnumerable<Square> squares)
    {
        var ship = new Ship(squares);
        ships.Add(ship);
    }

    public HitResult Hit(int row, int column)
    {
        throw new NotImplementedException();
    }
}
