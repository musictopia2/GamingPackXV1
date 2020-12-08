using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public partial class BlankClickableSquare
    {
        [Parameter]
        public EventCallback SpaceClicked { get; set; } //hopefully this works (?)
        [Parameter]
        public SizeF SpaceSize { get; set; }
        [Parameter]
        public PointF SpaceLocation { get; set; }
        [Parameter]
        public bool Fixed { get; set; }
        protected override bool ShouldRender()
        {
            return !Fixed;
        }
    }

}