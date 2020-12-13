using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using PickelCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace PickelCardGameBlazor
{
    public partial class TwoPlayerTrickBlazor
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumSuitList, PickelCardGameCardInformation>? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}