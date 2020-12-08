using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public partial class BorderedSpace
    {
        //hint:  maybe for next version, if i can figure out better naming, combine the controls.
        //bad news is unable to come up with a better name as of october 8, 2020.
        //in order to make it as easy as possible, then have 2 separate that is related but little different.
        //one has borders and the other is completely blank.
        //of course, can have other ones as well that is also a little different.

        public enum EnumShapeCategory
        {
            Rectangle,
            Oval //decided for oval instead of circle to be more flexible.  if you want circle, then make the width and height the same.
        }

        //looks like if spacesize is 0, then its intended for grid.


        [Parameter]
        public SizeF SpaceSize { get; set; } //this is for the size of the space that needs the border.
        [Parameter]
        public EnumShapeCategory ShapeCategory { get; set; }

        [Parameter]
        public EventCallback SpaceClicked { get; set; } //hopefully this works (?)

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public int BorderWidth { get; set; }

        [Parameter]
        public string BorderColor { get; set; } = ""; //if no bordercolor, then don't even use this.

        [Parameter]
        public string FillColor { get; set; } = cc.Transparent;

        [Parameter]
        public bool Fixed { get; set; }

        private async Task ProcessClickAsync()
        {
            if (SpaceClicked.HasDelegate == false)
            {
                return;
            }
            await SpaceClicked.InvokeAsync(null);
        }
        protected override bool ShouldRender()
        {
            return !Fixed;
        }
        [Parameter]
        public PointF SpaceLocation { get; set; }
        private string ViewBox()
        {
            var value = (BorderWidth / 2) * -1;
            return $"{value} {value} {SpaceSize.Width + BorderWidth} {SpaceSize.Height + BorderWidth}";
        }

        private float RadiusX => SpaceSize.Width / 2;
        private float RadiusY => SpaceSize.Height / 2;

        private string ShapeStyle()
        {
            string output = $"stroke: {BorderColor.ToWebColor()}; stroke-width: {BorderWidth}; stroke-miterlimit:4;";
            return output;
        }
    }
}