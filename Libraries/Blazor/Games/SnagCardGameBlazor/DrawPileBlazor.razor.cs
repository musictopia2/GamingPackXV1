using BasicGameFrameworkLibrary.DrawableListsObservable;
using SnagCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace SnagCardGameBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<SnagCardGameCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}