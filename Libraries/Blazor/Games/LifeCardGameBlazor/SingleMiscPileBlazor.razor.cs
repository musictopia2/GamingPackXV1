using BasicGameFrameworkLibrary.DrawableListsObservable;
using LifeCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace LifeCardGameBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<LifeCardGameCardInformation>? SinglePile { get; set; }
        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private string RealHeight => $"{TargetHeight}vh";
    }
}