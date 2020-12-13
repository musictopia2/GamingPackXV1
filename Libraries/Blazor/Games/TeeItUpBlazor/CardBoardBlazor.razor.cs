using BasicGameFrameworkLibrary.DrawableListsObservable;
using Microsoft.AspNetCore.Components;
using TeeItUpCP.Cards;
namespace TeeItUpBlazor
{
    public partial class CardBoardBlazor
    {
        [Parameter]
        public GameBoardObservable<TeeItUpCardInformation>? DataContext { get; set; }
    }
}