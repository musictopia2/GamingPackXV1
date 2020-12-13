using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Data;
using FiveCrownsCP.Logic;
using Microsoft.AspNetCore.Components;
namespace FiveCrownsBlazor
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
        public MainSetsObservable<EnumSuitList, EnumColorList, FiveCrownsCardInformation, PhaseSet, SavedSet>? DataContext { get; set; }
    }
}