using BasicGameFrameworkLibrary.DrawableListsObservable;
using SnagCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace SnagCardGameBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<SnagCardGameCardInformation>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";

        private string RealHeight => $"{TargetHeight}vh";
    }
}