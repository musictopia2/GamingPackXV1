using BasicGameFrameworkLibrary.DrawableListsObservable;
using ClueBoardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace ClueBoardGameBlazor
{
    public partial class ClueHandBlazor
    {
        [Parameter]
        public HandObservable<CardInfo>? Hand { get; set; }
    }
}