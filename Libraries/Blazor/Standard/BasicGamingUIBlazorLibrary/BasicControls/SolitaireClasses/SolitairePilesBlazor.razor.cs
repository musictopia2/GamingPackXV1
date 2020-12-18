using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using Microsoft.AspNetCore.Components;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
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

        private string GetColumns => aa.RepeatMinimum(MainPiles!.PileList.Count);
    }
}