using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using HuseHeartsCP.Cards;
using Microsoft.AspNetCore.Components;
namespace HuseHeartsBlazor
{
    public partial class TwoPlayerTrickBlazor
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumSuitList, HuseHeartsCardInformation>? DataContext { get; set; }

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}