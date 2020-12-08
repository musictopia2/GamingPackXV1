using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using BasicMultiplayerMiscCardGamesCP.Cards;
using BasicGameFrameworkLibrary.DrawableListsObservable;
namespace BasicMultiplayerMiscCardGamesBlazor
{
    public partial class SingleMiscPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public SingleObservablePile<BasicMultiplayerMiscCardGamesCardInformation>? SinglePile { get; set; }

        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";

        private string RealHeight => $"{TargetHeight}vh";

        private string GetKey => Guid.NewGuid().ToString();
    }
}