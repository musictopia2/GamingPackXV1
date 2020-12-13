using System.Drawing;
namespace PassOutDiceGameCP.Data
{
    public class SpaceInfo
    {
        public RectangleF Bounds { get; set; }
        public EnumColorChoice Color { get; set; }
        public int Player { get; set; }
        public bool IsEnabled { get; set; }
        public int FirstValue { get; set; }
        public int SecondValue { get; set; }
    }
}