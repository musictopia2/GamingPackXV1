using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using XPuzzleCP.Data;
using XPuzzleCP.Logic;
namespace XPuzzleCP.ViewModels
{
    [InstanceGame]
    public class XPuzzleMainViewModel : BlazorScreenViewModel, IBasicEnableProcess, IBlankGameVM, IAggregatorContainer
    {
        private readonly XPuzzleGameBoardClass _gameBoard;
        private readonly BasicData _basicData;
        [Command(EnumCommandCategory.Plain)]
        public async Task MakeMoveAsync(XPuzzleSpaceInfo space)
        {
            await _gameBoard!.MakeMoveAsync(space);
            EnumMoveList NextMove = _gameBoard.Results();
            if (NextMove == EnumMoveList.TurnOver)
            {
                return; //will automatically enable it again.
            }
            if (NextMove == EnumMoveList.Won)
            {
                await UIPlatform.ShowMessageAsync("Congratulations, you won");
                await this.SendGameOverAsync(); //only if you won obviously.
            }
        }
        public XPuzzleMainViewModel(CommandContainer commandContainer, XPuzzleGameBoardClass gameBoard, BasicData basicData)
        {
            CommandContainer = commandContainer;
            _gameBoard = gameBoard; //hopefully this works.  means you have to really rethink.
            _basicData = basicData;
            _basicData.GameDataLoading = true; //has to be here because multiplayer games will be different.
        }
        public CommandContainer CommandContainer { get; set; }
        IEventAggregator IAggregatorContainer.Aggregator => Aggregator;
        public bool CanEnableBasics()
        {
            return true; //because maybe you can't enable it.
        }
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            await _gameBoard.NewGameAsync();
            _basicData.GameDataLoading = false;
            CommandContainer.UpdateAll(); //maybe needed for when its new game.
        }
    }
}