using BasicGameFrameworkLibrary.DrawableListsObservable;
using FillOrBustCP.Cards;
using Microsoft.AspNetCore.Components;
namespace FillOrBustBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public DeckObservablePile<FillOrBustCardInformation>? DeckPile { get; set; }
        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";
        private string RealHeight => $"{TargetHeight}vh";
    }
}