using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicMultiplayerTrickCardGamesCP.Cards;
using Microsoft.AspNetCore.Components;
namespace BasicMultiplayerTrickCardGamesBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<BasicMultiplayerTrickCardGamesCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}