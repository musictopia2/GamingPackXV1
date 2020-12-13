using BasicGameFrameworkLibrary.DrawableListsObservable;
using SixtySix2PlayerCP.Cards;
using Microsoft.AspNetCore.Components;
namespace SixtySix2PlayerBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<SixtySix2PlayerCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}