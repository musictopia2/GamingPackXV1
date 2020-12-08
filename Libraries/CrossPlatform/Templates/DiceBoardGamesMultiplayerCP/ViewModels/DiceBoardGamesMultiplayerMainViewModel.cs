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
using DiceBoardGamesMultiplayerCP.Logic;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using DiceBoardGamesMultiplayerCP.Data;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace DiceBoardGamesMultiplayerCP.ViewModels
{
    [InstanceGame]
    public class DiceBoardGamesMultiplayerMainViewModel : BoardDiceGameVM
    {
        private readonly DiceBoardGamesMultiplayerMainGameClass _mainGame; //if we don't need, delete.
        private readonly DiceBoardGamesMultiplayerVMData _model; //if we don't need, delete.

        public DiceBoardGamesMultiplayerMainViewModel(CommandContainer commandContainer,
            DiceBoardGamesMultiplayerMainGameClass mainGame,
            DiceBoardGamesMultiplayerVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = model;
        }

        public DiceCup<SimpleDice> GetCup => _model.Cup!;

        //anything else needed is here.
        public override bool CanRollDice()
        {
            return base.CanRollDice();
        }
        public override async Task RollDiceAsync() //if any changes, do here.
        {
            await base.RollDiceAsync();
        }
    }
}