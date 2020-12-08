using System;
using System.Text;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using fs = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.FileHelpers;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.Messenging;
using SinglePlayerMiscGamesCP.Logic;
using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;

namespace SinglePlayerMiscGamesCP.ViewModels
{
    [InstanceGame]
    public class SinglePlayerMiscGamesMainViewModel : BlazorScreenViewModel, IBasicEnableProcess, IBlankGameVM, IAggregatorContainer
    {
        private readonly IEventAggregator _aggregator;
        private readonly SinglePlayerMiscGamesMainGameClass _mainGame;

        public SinglePlayerMiscGamesMainViewModel(IEventAggregator aggregator, CommandContainer commandContainer, IGamePackageResolver resolver)
        {
            _aggregator = aggregator;
            CommandContainer = commandContainer;
            _mainGame = resolver.ReplaceObject<SinglePlayerMiscGamesMainGameClass>(); //hopefully this works.  means you have to really rethink.
        }

        public CommandContainer CommandContainer { get; set; }

        IEventAggregator IAggregatorContainer.Aggregator => _aggregator;

        public bool CanEnableBasics()
        {
            return true; //because maybe you can't enable it.
        }
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            await _mainGame.NewGameAsync();
        }
    }
}
