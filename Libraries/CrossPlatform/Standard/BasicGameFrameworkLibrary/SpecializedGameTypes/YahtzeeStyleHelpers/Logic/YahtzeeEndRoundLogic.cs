using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public class YahtzeeEndRoundLogic<D> : IYahtzeeEndRoundLogic
        where D : SimpleDice, new()
    {
        private readonly YahtzeeGameContainer<D> _gameContainer;
        private readonly ScoreContainer _scoreContainer;

        public YahtzeeEndRoundLogic(YahtzeeGameContainer<D> gameContainer,
            ScoreContainer scoreContainer
            )
        {
            _gameContainer = gameContainer;
            _scoreContainer = scoreContainer;
        }
        private async Task GameOverAsync()
        {
            _gameContainer.SingleInfo = _gameContainer.PlayerList.OrderByDescending(items => items.Points).Take(1).Single();
            _gameContainer.SaveRoot!.Round = 0;
            _gameContainer.SaveRoot.RollNumber = 0; //i think
            await _gameContainer.ShowWinAsync!.Invoke();
        }
        public bool IsGameOver => _scoreContainer.RowList.Where(items => items.RowSection == EnumRow.Regular)
            .All(Items => Items.HasFilledIn() == true);
        async Task IYahtzeeEndRoundLogic.StartNewRoundAsync()
        {
            if (_scoreContainer.RowList.Count == 0)
            {
                throw new BasicBlankException("The row list of the score container cannot be 0.  Rethink");
            }
            if (_gameContainer.WhoTurn == _gameContainer.SaveRoot!.Begins)
            {
                if (IsGameOver == true || _gameContainer.Test.ImmediatelyEndGame) //to test new game quickly.
                {
                    await GameOverAsync();
                    return;
                }
                _gameContainer.SaveRoot.Round++;
            }
            await _gameContainer.StartNewTurnAsync!.Invoke();
        }
    }
}