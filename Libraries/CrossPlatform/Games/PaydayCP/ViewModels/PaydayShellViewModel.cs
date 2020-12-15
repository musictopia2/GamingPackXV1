using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using PaydayCP.Data;
using System.Threading.Tasks;

namespace PaydayCP.ViewModels
{
    public class PaydayShellViewModel : BasicBoardGamesShellViewModel<PaydayPlayerItem>
    {
        public PaydayShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test)
            : base(mainContainer, container, gameData, basicData, save, test)
        {
        }
        protected override void ReplaceVMData()
        {
            //ignore but reserves the right to do something else if needed.
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<PaydayMainViewModel>();
            return model;
        }
        protected override async Task ShowNewGameAsync()
        {
            await UIPlatform.ShowMessageAsync("Game is over.  However, unable to allow for new game.  If you want new game, close out and reconnect again.");
        }
    }
}