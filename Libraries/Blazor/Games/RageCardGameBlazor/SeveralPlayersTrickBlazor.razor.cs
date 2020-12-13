using Microsoft.AspNetCore.Components;
using RageCardGameCP.Logic;
namespace RageCardGameBlazor
{
    public partial class SeveralPlayersTrickBlazor
    {
        [Parameter]
        public SpecificTrickAreaObservable? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public bool ExtraLongSecondColumn { get; set; } = false;
        private string RealHeight => $"{TargetHeight}vh";
    }
}