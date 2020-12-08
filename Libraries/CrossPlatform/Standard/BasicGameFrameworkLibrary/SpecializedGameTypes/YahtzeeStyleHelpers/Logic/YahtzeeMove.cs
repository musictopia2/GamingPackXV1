using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public class YahtzeeMove<D> : IYahtzeeMove, IMoveNM
        where D : SimpleDice, new()
    {
        private readonly ScoreContainer _scoreContainer;
        private readonly IScoreLogic _scoreLogic;
        private readonly YahtzeeVMData<D> _model;
        private readonly IYahtzeeEndRoundLogic _endRoundLogic;
        private readonly YahtzeeGameContainer<D> _gameContainer;
        public YahtzeeMove(ScoreContainer scoreContainer,
            IScoreLogic scoreLogic,
            YahtzeeVMData<D> model,
            IYahtzeeEndRoundLogic endRoundLogic,
            YahtzeeGameContainer<D> gameContainer)
        {
            _scoreContainer = scoreContainer;
            _scoreLogic = scoreLogic;
            _model = model;
            _endRoundLogic = endRoundLogic;
            _gameContainer = gameContainer;
        }
        public async Task MakeMoveAsync(RowInfo row)
        {
            _scoreLogic.MarkScore(row);
            _gameContainer.SingleInfo!.Points = _scoreLogic.TotalScore;
            _gameContainer.SingleInfo.RowList = _scoreContainer.RowList.ToCustomBasicList();
            _gameContainer.Command.UpdateAll();
            if (_gameContainer.Test.NoAnimations == false)
            {
                await (_gameContainer.Delay.DelaySeconds(1));
            }
            _model.Cup!.UnholdDice();
            _model.Cup.HideDice();
            if (_gameContainer.PlayerList!.Any(x => x.MissNextTurn))
            {
                ToastPlatform.ShowInfo($"Everyone gets their turns skipped except for {_gameContainer.SingleInfo.NickName}.  Also, everyone will get a 0 for the category closest to the top because {_gameContainer.SingleInfo.NickName} got a Kismet even though it was already marked");
            }
            await _gameContainer.EndTurnAsync!.Invoke();
            await _endRoundLogic.StartNewRoundAsync();
        }
        public async Task MoveReceivedAsync(string data)
        {
            int id = int.Parse(data);
            RowInfo row = _scoreContainer.RowList[id];
            await MakeMoveAsync(row);
        }
    }
}