using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Base
{
    public partial class BorderedCommandSpace
    {
        //this is used for cases where its like a bordered space.  except it is smart enough to use commands.

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public int BorderWidth { get; set; }
        [Parameter]
        public string BorderColor { get; set; } = ""; //if no bordercolor, then don't even use this.
        [Parameter]
        public string FillColor { get; set; } = cc.Transparent;
        private string ShapeStyle()
        {
            string output = $"stroke: {BorderColor.ToWebColor()}; stroke-width: {BorderWidth}; stroke-miterlimit:4;";
            return output;
        }
    }
}