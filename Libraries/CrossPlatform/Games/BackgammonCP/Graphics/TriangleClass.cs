using CommonBasicStandardLibraries.CollectionClasses;
using System.Drawing;
namespace BackgammonCP.Graphics
{
    public class TriangleClass
    {
        public int NumberOfTiles { get; set; }
        public int PlayerOwns { get; set; }
        public CustomBasicList<PointF> Locations { get; set; } = new CustomBasicList<PointF>();
    }
}