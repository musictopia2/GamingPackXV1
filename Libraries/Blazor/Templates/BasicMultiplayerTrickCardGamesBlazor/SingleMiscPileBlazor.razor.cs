using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicMultiplayerTrickCardGamesCP.Cards;
using Microsoft.AspNetCore.Components;
namespace BasicMultiplayerTrickCardGamesBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<BasicMultiplayerTrickCardGamesCardInformation>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";

        private string RealHeight => $"{TargetHeight}vh";
    }
}