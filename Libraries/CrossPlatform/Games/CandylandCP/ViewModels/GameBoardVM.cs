using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using CandylandCP.Logic;
using System.Threading.Tasks;
namespace CandylandCP.ViewModels
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardVM
    {
        public GameBoardVM(CandylandMainGameClass mainGame, CommandContainer command)
        {
            _mainGame = mainGame;
            _command = command;
        }

        private readonly CandylandMainGameClass _mainGame;
        private readonly CommandContainer _command;
        public async Task CastleAsync()
        {
            _command.IsExecuting = true;
            await _mainGame.GameOverAsync();
        }
        public async Task MakeMoveAsync(int space)
        {
            _command.IsExecuting = true;
            await _mainGame!.MakeMoveAsync(space);
        }
        public bool IsCastleValid
        {

            get
            {
                if (_mainGame.GameBoard1.IsValidMove(127, _mainGame.SaveRoot.CurrentCard!.WhichCard, _mainGame, _mainGame.SaveRoot.CurrentCard.HowMany))
                {
                    return true;
                }
                return false;
            }
        }
        public int GetValidSpaceMove
        {
            get
            {
                for (int x = 1; x <= 126; x++)
                {
                    if (_mainGame.GameBoard1.IsValidMove(x, _mainGame.SaveRoot.CurrentCard!.WhichCard, _mainGame, _mainGame.SaveRoot.CurrentCard.HowMany))
                    {
                        return x;
                    }
                }
                return 0;
            }
        }
    }
}