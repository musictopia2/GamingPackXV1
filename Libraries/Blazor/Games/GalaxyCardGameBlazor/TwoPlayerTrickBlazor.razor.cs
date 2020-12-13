using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using GalaxyCardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace GalaxyCardGameBlazor
{
    public partial class TwoPlayerTrickBlazor
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumSuitList, GalaxyCardGameCardInformation>? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}