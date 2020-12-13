using LifeBoardGameCP.Data;
using System.Drawing;
namespace LifeBoardGameCP.Graphics
{
    public class EndPositionInfo
    {
        public LifeBoardGamePlayerItem? Player { get; set; }
        public RectangleF Bounds { get; set; }
    }
}