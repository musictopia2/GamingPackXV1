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
using BasicMultiplayerDiceGamesCP.Logic;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.Dice;
using BasicMultiplayerDiceGamesCP.Data;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
//i think this is the most common things i like to do
namespace BasicMultiplayerDiceGamesCP.ViewModels
{
    [InstanceGame]
    public class BasicMultiplayerDiceGamesMainViewModel : DiceGamesVM<SimpleDice>
    {
        private readonly BasicMultiplayerDiceGamesMainGameClass _mainGame; //if we don't need, delete.
        private readonly BasicMultiplayerDiceGamesVMData _model;
        public BasicMultiplayerDiceGamesMainViewModel(CommandContainer commandContainer,
            BasicMultiplayerDiceGamesMainGameClass mainGame,
            BasicMultiplayerDiceGamesVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = viewModel;
        }

        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        public PlayerCollection<BasicMultiplayerDiceGamesPlayerItem> PlayerList => _mainGame.PlayerList;
        //anything else needed is here.
        protected override bool CanEnableDice()
        {
            return false; //if you can enable dice, change the routine.
        }
        public override bool CanEndTurn()
        {
            return true; //if you can't or has extras, do here.
        }
        public override bool CanRollDice()
        {
            return base.CanRollDice(); //anything you need to figure out if you can roll is here.
        }

    }
}