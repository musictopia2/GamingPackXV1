using BasicGameFrameworkLibrary.DrawableListsObservable;
using GalaxyCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace GalaxyCardGameBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<GalaxyCardGameCardInformation>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";

        private string RealHeight => $"{TargetHeight}vh";
    }
}