namespace Model;

public class ShotsGrid : Grid
{
    public ShotsGrid(int rows, int columns) : base(rows, columns)
    {
    }

    protected override bool IsSquareAvailable(int row, int column)
    {
        throw new NotImplementedException();
    }
}