using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using Microsoft.AspNetCore.Components;
namespace LittleSpiderSolitaireBlazor
{
    public partial class CustomWasteUI
    {
        [Parameter]
        public BasicMultiplePilesCP<SolitaireCard>? MainPiles { get; set; }

        [Parameter]
        public BasicMultiplePilesCP<SolitaireCard>? WastePiles { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; }
    }
}