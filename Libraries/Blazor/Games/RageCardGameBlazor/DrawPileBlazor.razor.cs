using BasicGameFrameworkLibrary.DrawableListsObservable;
using RageCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace RageCardGameBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<RageCardGameCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}