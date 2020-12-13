using Microsoft.AspNetCore.Components;
using RookCP.Logic;
namespace RookBlazor
{
    public partial class SeveralPlayersTrickBlazor
    {
        [Parameter]
        public RookTrickAreaCP? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public bool ExtraLongSecondColumn { get; set; } = false;

        private string RealHeight => $"{TargetHeight}vh";
    }
}