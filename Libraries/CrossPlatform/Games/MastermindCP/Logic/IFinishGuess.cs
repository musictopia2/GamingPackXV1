using MastermindCP.ViewModels;
using System.Threading.Tasks;

namespace MastermindCP.Logic
{
    public interface IFinishGuess
    {
        Task FinishGuessAsync(int howManyCorrect, GameBoardViewModel board);
    }
}