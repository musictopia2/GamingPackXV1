using System.Drawing;
namespace ConnectTheDotsCP.Graphics
{
    public class SquareInfo
    {
        public int Player { get; set; }
        public RectangleF Rectangle { get; set; }
        public int Color { get; set; } // blue or red.
        public int Column { get; set; }
        public int Row { get; set; }
    }
}