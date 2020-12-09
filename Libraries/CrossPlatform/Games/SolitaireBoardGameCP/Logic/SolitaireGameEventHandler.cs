using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using SolitaireBoardGameCP.Data;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace SolitaireBoardGameCP.Logic
{
    [SingletonGame]
    public class SolitaireGameEventHandler : ISolitaireBoardEvents
    {
        async Task ISolitaireBoardEvents.PiecePlacedAsync(GameSpace space, SolitaireBoardGameMainGameClass game)
        {
            if (game.IsValidMove(space) == false)
            {
                await UIPlatform.ShowMessageAsync("Illegal Move");
                await game.UnselectPieceAsync(space);
                return;
            }
            await game.MakeMoveAsync(space);
        }

        async Task ISolitaireBoardEvents.PieceSelectedAsync(GameSpace space, SolitaireBoardGameMainGameClass game)
        {
            if (space.Vector.Equals(game.PreviousPiece) == false)
            {
                await game.HightlightSpaceAsync(space);
                return;
            }
            game.SelectUnSelectSpace(space);
        }
    }
}
