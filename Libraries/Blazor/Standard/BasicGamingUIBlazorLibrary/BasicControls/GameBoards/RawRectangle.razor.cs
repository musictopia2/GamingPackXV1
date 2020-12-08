using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public partial class RawRectangle
    {
        [Parameter]
        public RectangleF Rectangle { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}