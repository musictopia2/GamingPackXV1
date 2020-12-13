using System.Drawing;
namespace RollEmCP.Data
{
    public class NumberInfo
    {
        public RectangleF Bounds { get; set; }
        public int Number { get; set; }
        public bool IsCrossed { get; set; }
        public bool Recently { get; set; }
    }
}