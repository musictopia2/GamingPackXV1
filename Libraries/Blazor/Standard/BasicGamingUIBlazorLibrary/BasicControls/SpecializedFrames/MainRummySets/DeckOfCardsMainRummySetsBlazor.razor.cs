using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.MainRummySets
{
    public partial class DeckOfCardsMainRummySetsBlazor<RU, SE, T>
            where RU : class, IRummmyObject<EnumSuitList, EnumRegularColorList>, IRegularCard, new()
            where SE : SetInfo<EnumSuitList, EnumRegularColorList, RU, T>
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
        public MainSetsObservable<EnumSuitList, EnumRegularColorList, RU, SE, T>? DataContext { get; set; } //hopefully this simple.
    }
}