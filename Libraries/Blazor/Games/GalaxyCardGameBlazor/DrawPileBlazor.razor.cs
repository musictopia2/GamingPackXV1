using BasicGameFrameworkLibrary.DrawableListsObservable;
using GalaxyCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace GalaxyCardGameBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<GalaxyCardGameCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}