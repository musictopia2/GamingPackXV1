using BasicGameFrameworkLibrary.DrawableListsObservable;
using SixtySix2PlayerCP.Cards;
using Microsoft.AspNetCore.Components;
namespace SixtySix2PlayerBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<SixtySix2PlayerCardInformation>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";

        private string RealHeight => $"{TargetHeight}vh";
    }
}