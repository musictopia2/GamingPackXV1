using BasicGameFrameworkLibrary.DrawableListsObservable;
using HorseshoeCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace HorseshoeCardGameBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<HorseshoeCardGameCardInformation>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";

        private string RealHeight => $"{TargetHeight}vh";
    }
}