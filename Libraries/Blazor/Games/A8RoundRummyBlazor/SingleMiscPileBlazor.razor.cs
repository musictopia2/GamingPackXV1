using A8RoundRummyCP.Cards;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using Microsoft.AspNetCore.Components;
namespace A8RoundRummyBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<A8RoundRummyCardInformation>? SinglePile { get; set; }
        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private string RealHeight => $"{TargetHeight}vh";
    }
}