using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.CardBoards
{
    public partial class DeckOfCardsCardBoardBlazor<R>
        where R : class, IRegularCard, new()
    {
        [Parameter]
        public GameBoardObservable<R>? DataContext { get; set; } //only iffy part is new game.
    }
}