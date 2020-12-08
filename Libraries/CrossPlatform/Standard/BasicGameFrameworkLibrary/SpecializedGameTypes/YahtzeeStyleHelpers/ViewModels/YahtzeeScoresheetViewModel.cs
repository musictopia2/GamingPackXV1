using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels
{
    public class YahtzeeScoresheetViewModel<D> : BlazorScreenViewModel, IBlankGameVM, IHandleAsync<SelectionChosenEventModel>, IScoresheetAction
        where D : SimpleDice, new()
    {
        private readonly ScoreContainer _scoreContainer;
        private readonly IEventAggregator _aggregator;
        private readonly YahtzeeGameContainer<D> _gameContainer;
        private readonly IYahtzeeMove _yahtzeeMove;
        private RowInfo? _privateChosen;

        public YahtzeeScoresheetViewModel(
            CommandContainer commandContainer,
            ScoreContainer scoreContainer,
            IEventAggregator aggregator,
            YahtzeeGameContainer<D> gameContainer,
            IYahtzeeMove yahtzeeMove
            )
        {
            CommandContainer = commandContainer;
            _scoreContainer = scoreContainer;
            _aggregator = aggregator;
            _gameContainer = gameContainer;
            _yahtzeeMove = yahtzeeMove;
        }

        public CommandContainer CommandContainer { get; set; }

        public async Task HandleAsync(SelectionChosenEventModel message)
        {
            _aggregator.Unsubscribe(this);
            switch (message.OptionChosen)
            {
                case EnumOptionChosen.Yes:
                    await ProcessMoveAsync(_privateChosen!);
                    break;
                case EnumOptionChosen.No:
                    _gameContainer.Command.ManuelFinish = false;
                    _gameContainer.Command.IsExecuting = false;
                    break;
                default:
                    throw new BasicBlankException("Should have chosen yes or no");
            }
        }



        private bool HasWarning(RowInfo currentRow) //done i think.
        {

            return !currentRow.Possible.HasValue;

        }

        public bool CanRow(RowInfo row)
        {
            if (_gameContainer.SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                return false;
            }
            if (_gameContainer.SaveRoot.RollNumber == 1)
            {
                return false; //hopefully this simple this time.
            }
            return !row.HasFilledIn(); //hopefully this simple now.
        }
        [Command(EnumCommandCategory.Plain)]
        public async Task RowAsync(RowInfo row)
        {
            _privateChosen = row;
            _gameContainer.Command.ManuelFinish = true;
            if (HasWarning(row))
            {
                WarningEventModel warn = new WarningEventModel();
                warn.Message = "Are you sure you want to mark off " + row.Description;
                _aggregator.Subscribe(this);
                await _aggregator.PublishAsync(warn);
                return;
            }
            await ProcessMoveAsync(row);
        }

        private async Task ProcessMoveAsync(RowInfo row)
        {
            if (_privateChosen == null)
            {
                throw new BasicBlankException("Did not save selection");
            }
            _privateChosen = null;
            if (_gameContainer.CanSendMessage())
            {
                await _gameContainer.Network!.SendMoveAsync(_scoreContainer.RowList.IndexOf(row));
            }
            await _yahtzeeMove.MakeMoveAsync(row);
        }


    }
}