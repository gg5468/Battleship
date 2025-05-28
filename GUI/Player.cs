using System;
using Model;

namespace GUI;

public class Player
{
    public Fleet PlayerFleet;
    public Gunnery Gunnery;
    
    public int ShipsSunken { get; set; } = 0;

    public Player(int gridRows, int gridColumns, int[] shipLengths)
    {
        var fleetBuilder = new FleetBuilder(gridRows, gridColumns, shipLengths);
        PlayerFleet = fleetBuilder.CreateFleet();
        Gunnery = new Gunnery(gridRows, gridColumns, shipLengths);
    }

}
