using BasicGameFrameworkLibrary.DrawableListsObservable;
using RoundsCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace RoundsCardGameBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<RoundsCardGameCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}