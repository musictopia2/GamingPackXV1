using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using FlinchCP.Cards;
using Microsoft.AspNetCore.Components;
namespace FlinchBlazor
{
    public partial class MultiplePilesBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public BasicMultiplePilesCP<FlinchCardInformation>? Piles { get; set; }
        [Parameter]
        public string AnimationTag { get; set; } = "";
        [Parameter]
        public bool Inline { get; set; } = true;
        private string RealHeight => TargetHeight.HeightString();
    }
}