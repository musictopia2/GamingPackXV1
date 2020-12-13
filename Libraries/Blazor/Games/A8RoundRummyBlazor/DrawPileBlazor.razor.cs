using A8RoundRummyCP.Cards;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using Microsoft.AspNetCore.Components;
namespace A8RoundRummyBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public DeckObservablePile<A8RoundRummyCardInformation>? DeckPile { get; set; }
        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";
        private string RealHeight => $"{TargetHeight}vh";
    }
}