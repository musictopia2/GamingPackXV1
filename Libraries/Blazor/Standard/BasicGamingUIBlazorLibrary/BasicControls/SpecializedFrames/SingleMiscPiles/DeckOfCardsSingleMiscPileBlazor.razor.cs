using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.SingleMiscPiles
{
    public partial class DeckOfCardsSingleMiscPileBlazor<R>
        where R : class, IRegularCard, new()
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<R>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private string RealHeight => $"{TargetHeight}vh";
    }
}