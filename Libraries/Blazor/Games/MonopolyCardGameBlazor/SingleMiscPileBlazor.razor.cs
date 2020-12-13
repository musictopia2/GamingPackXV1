using BasicGameFrameworkLibrary.DrawableListsObservable;
using Microsoft.AspNetCore.Components;
using MonopolyCardGameCP.Cards;
namespace MonopolyCardGameBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<MonopolyCardGameCardInformation>? SinglePile { get; set; }
        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private string RealHeight => $"{TargetHeight}vh";
    }
}