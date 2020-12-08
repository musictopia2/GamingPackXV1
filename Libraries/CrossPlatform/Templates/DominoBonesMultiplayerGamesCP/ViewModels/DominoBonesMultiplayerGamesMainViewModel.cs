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
using DominoBonesMultiplayerGamesCP.Logic;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using DominoBonesMultiplayerGamesCP.Data;
//i think this is the most common things i like to do
namespace DominoBonesMultiplayerGamesCP.ViewModels
{
    [InstanceGame]
    public class DominoBonesMultiplayerGamesMainViewModel : DominoGamesVM<SimpleDominoInfo>
    {
        private readonly DominoBonesMultiplayerGamesMainGameClass _mainGame; //if we don't need, delete.
        private readonly IDominoGamesData<SimpleDominoInfo> _viewModel;

        public DominoBonesMultiplayerGamesMainViewModel(
            CommandContainer commandContainer,
            DominoBonesMultiplayerGamesMainGameClass mainGame,
            IDominoGamesData<SimpleDominoInfo> viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver) : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _viewModel = viewModel;
        }
        public HandObservable<SimpleDominoInfo> PlayerHand => _viewModel.PlayerHand1;
        public DominosBoneYardClass<SimpleDominoInfo> BoneYard => _viewModel.BoneYard;
        public PlayerCollection<DominoBonesMultiplayerGamesPlayerItem> GetPlayerList => _mainGame.SaveRoot.PlayerList;
        protected override bool CanEnableBoneYard()
        {
            return true;
        }
    }
}