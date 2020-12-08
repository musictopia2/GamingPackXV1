namespace BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers
{
    public class CheckersChessVector
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public CheckersChessVector(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}