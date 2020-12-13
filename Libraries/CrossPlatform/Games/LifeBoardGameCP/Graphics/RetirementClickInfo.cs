using LifeBoardGameCP.Data;
using System.Drawing;
namespace LifeBoardGameCP.Graphics
{
    public class RetirementClickInfo
    {
        public RectangleF Bounds { get; set; }
        public EnumFinal OptionChosen { get; set; }
        public RetirementClickInfo(RectangleF bounds, EnumFinal optionChosen)
        {
            OptionChosen = optionChosen;
            Bounds = bounds;
        }
    }
}