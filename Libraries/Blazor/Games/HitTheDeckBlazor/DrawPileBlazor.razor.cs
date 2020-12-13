using BasicGameFrameworkLibrary.DrawableListsObservable;
using HitTheDeckCP.Cards;
using Microsoft.AspNetCore.Components;
namespace HitTheDeckBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public DeckObservablePile<HitTheDeckCardInformation>? DeckPile { get; set; }
        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";
        private string RealHeight => $"{TargetHeight}vh";
    }
}