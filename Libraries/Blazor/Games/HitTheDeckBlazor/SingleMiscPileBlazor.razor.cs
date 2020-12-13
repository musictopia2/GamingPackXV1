using BasicGameFrameworkLibrary.DrawableListsObservable;
using HitTheDeckCP.Cards;
using Microsoft.AspNetCore.Components;
using System;
namespace HitTheDeckBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<HitTheDeckCardInformation>? SinglePile { get; set; }
        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private string RealHeight => $"{TargetHeight}vh";
    }
}