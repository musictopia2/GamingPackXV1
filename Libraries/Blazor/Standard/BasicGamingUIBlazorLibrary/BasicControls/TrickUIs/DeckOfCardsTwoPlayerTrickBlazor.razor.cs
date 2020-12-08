using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.TrickUIs
{
    public partial class DeckOfCardsTwoPlayerTrickBlazor<T>
        where T : class, IRegularCard, ITrickCard<EnumSuitList>, new()
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumSuitList, T>? DataContext { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
        //if deck is not good enough, rethink.  can't use guid because causes performance problems.
    }
}