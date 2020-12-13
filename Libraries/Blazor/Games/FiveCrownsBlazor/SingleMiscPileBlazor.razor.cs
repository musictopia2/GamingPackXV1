using BasicGameFrameworkLibrary.DrawableListsObservable;
using FiveCrownsCP.Cards;
using Microsoft.AspNetCore.Components;
using System;
namespace FiveCrownsBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<FiveCrownsCardInformation>? SinglePile { get; set; }
        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private string RealHeight => $"{TargetHeight}vh";
        private string GetKey => Guid.NewGuid().ToString();
    }
}