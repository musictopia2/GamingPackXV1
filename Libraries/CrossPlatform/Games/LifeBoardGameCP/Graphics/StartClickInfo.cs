using LifeBoardGameCP.Data;
using System.Drawing;
namespace LifeBoardGameCP.Graphics
{
    public class StartClickInfo
    {
        public RectangleF Bounds { get; set; }
        public EnumStart OptionChosen { get; set; }
        public StartClickInfo(RectangleF bounds, EnumStart optionChosen)
        {
            Bounds = bounds;
            OptionChosen = optionChosen;
        }
    }
}