using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using BasicMultiplayerTrickCardGamesCP.Cards;
using Microsoft.AspNetCore.Components;
namespace BasicMultiplayerTrickCardGamesBlazor
{
    public partial class TwoPlayerTrickBlazor
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumSuitList, BasicMultiplayerTrickCardGamesCardInformation>? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}