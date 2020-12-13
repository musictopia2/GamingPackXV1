using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using Microsoft.AspNetCore.Components;
using Phase10CP.Cards;
using Phase10CP.SetClasses;
namespace Phase10Blazor
{
    public partial class MainRummySetsBlazor
    {
        [Parameter]
        public string ContainerWidth { get; set; } = "";
        [Parameter]
        public string ContainerHeight { get; set; } = "";
        [Parameter]
        public double Divider { get; set; } = 1;
        [Parameter]
        public double AdditionalSpacing { get; set; } = -5;
        [Parameter]
        public MainSetsObservable<EnumColorTypes, EnumColorTypes, Phase10CardInformation, PhaseSet, SavedSet>? DataContext { get; set; } //hopefully this simple.
    }
}