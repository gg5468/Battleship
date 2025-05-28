using System;
using Model;

namespace GUI;

public class Player
{
    public Fleet PlayerFleet;

    public Player(int gridRows, int gridColumns, int[] shipLengths)
    {
        var fleetBuilder = new FleetBuilder(gridRows, gridColumns, shipLengths);
        PlayerFleet = fleetBuilder.CreateFleet();
    }

}
