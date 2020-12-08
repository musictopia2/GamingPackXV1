namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public struct Vector //it does have to be a structure though.
    {
        public int Column { get; set; } //hopefully i can still do as vector and not have to come up with another name (?)
        public int Row { get; set; }
        public Vector(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}