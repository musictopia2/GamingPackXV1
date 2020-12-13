using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.Exceptions;
using GolfCardGameCP.Data;
using GolfCardGameCP.Logic;
using System.Threading.Tasks;
namespace GolfCardGameCP.ViewModels
{
    public class GolfCardGameShellViewModel : BasicMultiplayerShellViewModel<GolfCardGamePlayerItem>
    {
        public GolfCardGameShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test,
            GolfDelegates delegates
            )
            : base(mainContainer, container, gameData, basicData, save, test)
        {
            delegates.LoadMainScreenAsync = LoadMainScreenAsync;
        }
        public FirstViewModel? FirstScreen { get; set; }
        protected override async Task StartNewGameAsync()
        {
            if (MainVM != null)
            {
                await CloseSpecificChildAsync(MainVM);
                MainVM = null;
            }
            if (FirstScreen != null)
            {
                throw new BasicBlankException("First Screen should be null when loading First Screens");
            }
            FirstScreen = MainContainer.Resolve<FirstViewModel>();
            await LoadScreenAsync(FirstScreen);
        }
        private async Task LoadMainScreenAsync()
        {
            if (FirstScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(FirstScreen);
            FirstScreen = null;
            MainVM = MainContainer.Resolve<GolfCardGameMainViewModel>();
            await LoadScreenAsync(MainVM);
        }
        protected override IMainScreen GetMainViewModel()
        {
            throw new BasicBlankException("Needed to open first screen instead");
        }
    }
}