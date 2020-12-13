using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using Spades2PlayerCP.Cards;
using Microsoft.AspNetCore.Components;
namespace Spades2PlayerBlazor
{
    public partial class TwoPlayerTrickBlazor
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumSuitList, Spades2PlayerCardInformation>? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}