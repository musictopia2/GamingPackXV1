using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.SolitaireClasses
{
    public partial class SolitairePilesBlazor
    {
        [Parameter]
        public SolitairePilesCP? MainPiles { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// this is where you decide where you place it after.  useful for a game like eagle wings solitaire.
        /// is one based.
        /// </summary>
        [Parameter]
        public int PlaceAfter { get; set; }
    }
}