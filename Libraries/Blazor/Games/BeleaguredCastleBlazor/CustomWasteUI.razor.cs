using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using Microsoft.AspNetCore.Components;
namespace BeleaguredCastleBlazor
{
    public partial class CustomWasteUI
    {
        [Parameter]
        public SolitairePilesCP? WastePiles { get; set; }
        [Parameter]
        public BasicMultiplePilesCP<SolitaireCard>? MainPiles { get; set; }
    }
}