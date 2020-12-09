using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using SolitaireBoardGameCP.Data;
using SolitaireBoardGameCP.Logic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.

namespace SolitaireBoardGameCP.ViewModels
{
    [InstanceGame]
    public class SolitaireBoardGameMainViewModel : BlazorScreenViewModel, IBasicEnableProcess, IBlankGameVM, IAggregatorContainer
    {
        private readonly IEventAggregator _aggregator;
        private readonly BasicData _basicData;
        private readonly SolitaireBoardGameMainGameClass _mainGame;
        public SolitaireBoardGameMainViewModel(IEventAggregator aggregator, CommandContainer commandContainer, IGamePackageResolver resolver, BasicData basicData)
        {
            _aggregator = aggregator;
            CommandContainer = commandContainer;
            _basicData = basicData;
            _mainGame = resolver.ReplaceObject<SolitaireBoardGameMainGameClass>();
        }
        public CommandContainer CommandContainer { get; set; }
        IEventAggregator IAggregatorContainer.Aggregator => _aggregator;

        [Command(EnumCommandCategory.Plain)]
        public async Task MakeMoveAsync(GameSpace space)
        {
            await _mainGame.ProcessCommandAsync(space);
        }

        public bool CanEnableBasics()
        {
            return true; //because maybe you can't enable it.
        }
        protected override async Task ActivateAsync()
        {
            _basicData.GameDataLoading = true;
            await base.ActivateAsync();
            await _mainGame.NewGameAsync();
            _basicData.GameDataLoading = false;
        }
    }
}
