using Microsoft.AspNetCore.Components;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BackgammonBlazor
{
    public partial class BackgammonHintSpaceBlazor
    {
        [Parameter]
        public RectangleF Bounds { get; set; }
        [Parameter]
        public string BorderColor { get; set; } = cc.Transparent;
        [Parameter]
        public int SpaceNumber { get; set; } //this will determine what is rendered.
    }
}