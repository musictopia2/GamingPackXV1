using System.Drawing;
namespace ConnectTheDotsCP.Graphics
{
    public class LineInfo
    {
        public PointF StartingPoint { get; set; }
        public PointF FinishingPoint { get; set; } //needs to be public now so blazor can communicate with it now.
        public bool IsTaken { get; set; } = false;
        public int DotRow1 { get; set; }
        public int DotRow2 { get; set; }
        public int DotColumn1 { get; set; }
        public int DotColumn2 { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Horizontal { get; set; }
        public int Index { get; set; } // needed for autoresume
    }
}
