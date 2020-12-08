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
using BasicMultiplayerMiscCardGamesCP.Logic;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicMultiplayerMiscCardGamesCP.Data;
using BasicMultiplayerMiscCardGamesCP.Cards;
//i think this is the most common things i like to do
namespace BasicMultiplayerMiscCardGamesCP.ViewModels
{
    [InstanceGame]
    public class BasicMultiplayerMiscCardGamesMainViewModel : BasicCardGamesVM<BasicMultiplayerMiscCardGamesCardInformation>
    {
        private readonly BasicMultiplayerMiscCardGamesMainGameClass _mainGame; //if we don't need, delete.
        private readonly BasicMultiplayerMiscCardGamesVMData _model;
        private readonly BasicMultiplayerMiscCardGamesGameContainer _gameContainer; //if not needed, delete.

        public BasicMultiplayerMiscCardGamesMainViewModel(CommandContainer commandContainer,
            BasicMultiplayerMiscCardGamesMainGameClass mainGame,
            BasicMultiplayerMiscCardGamesVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            BasicMultiplayerMiscCardGamesGameContainer gameContainer
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _gameContainer = gameContainer;
            _model.Deck1.NeverAutoDisable = true;
        }
        //anything else needed is here.
        protected override bool CanEnableDeck()
        {
            //todo:  decide whether to enable deck.
            return false; //otherwise, can't compile.
        }

        protected override bool CanEnablePile1()
        {
            //todo:  decide whether to enable deck.
            return false; //otherwise, can't compile.
        }

        protected override async Task ProcessDiscardClickedAsync()
        {
            //if we have anything, will be here.
            await Task.CompletedTask;
        }
        public override bool CanEnableAlways()
        {
            return true;
        }
    }
}