using System;
using System.Linq;
using System.Net.Http;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using Microsoft.AspNetCore.Components;
using BasicMultiplayerMiscCardGamesCP.Cards;

namespace BasicMultiplayerMiscCardGamesBlazor
{
    public partial class DrawPileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;

        [Parameter]
        public DeckObservablePile<BasicMultiplayerMiscCardGamesCardInformation>? DeckPile { get; set; }

        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";

        private string RealHeight => $"{TargetHeight}vh";
    }
}