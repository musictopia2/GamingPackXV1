using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CandylandCP.Data;
using CandylandCP.Logic;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
namespace CandylandCP.ViewModels
{
    [InstanceGame]
    public class CandylandMainViewModel : BasicMultiplayerMainVM
    {
        private readonly CandylandMainGameClass _mainGame;

        public CandylandMainViewModel(CommandContainer commandContainer,
            CandylandMainGameClass mainGame,
            IViewModelData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
        }
        public CustomBasicList<CandylandPlayerItem> GetPlayerList
        {
            get
            {
                CustomBasicList<CandylandPlayerItem> output = _mainGame.PlayerList.ToCustomBasicList();
                output.RemoveSpecificItem(_mainGame.SingleInfo!);
                output.Add(_mainGame.SingleInfo!);
                return output;
            }
        }
        public CandylandPlayerItem CurrentPlayer => _mainGame.SingleInfo!;
        public CandylandCardData CurrentCard => _mainGame.SaveRoot.CurrentCard!;
    }
}