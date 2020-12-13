using System.Drawing;
namespace LifeBoardGameCP.Graphics
{
    public class ChooseSpaceInfo
    {
        public RectangleF Bounds { get; set; }
        public string Color { get; set; } = ""; //go ahead and let the cross platform figure out the color which blazor will render.
        public int Space { get; set; }
    }
}