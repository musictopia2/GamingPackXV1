using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using System.Threading.Tasks;
using UnoCP.Data;
using UnoCP.Logic;
namespace UnoCP.ViewModels
{
    public class UnoShellViewModel : BasicMultiplayerShellViewModel<UnoPlayerItem>
    {
        public UnoShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test,
            UnoColorsDelegates delegates
            )
            : base(mainContainer, container, gameData, basicData, save, test)
        {
            delegates.OpenColorAsync = OpenColorAsync;
            delegates.CloseColorAsync = CloseColorAsync;
        }
        public ChooseColorViewModel? ColorScreen { get; set; }
        private async Task OpenColorAsync()
        {
            if (MainVM != null)
            {
                await CloseSpecificChildAsync(MainVM);
                MainVM = null;
            }
            if (ColorScreen != null)
            {
                await CloseSpecificChildAsync(ColorScreen);
                ColorScreen = null;
            }
            ColorScreen = MainContainer.Resolve<ChooseColorViewModel>();
            await LoadScreenAsync(ColorScreen);
        }
        private async Task CloseColorAsync()
        {
            if (ColorScreen == null && MainVM != null)
            {
                return;
            }
            if (ColorScreen != null)
            {
                await CloseSpecificChildAsync(ColorScreen);
                ColorScreen = null;
            }
            await StartNewGameAsync(); //misleading because sometimes it can reload even if not new game like in cases like uno.
        }
        protected override async Task GetStartingScreenAsync()
        {
            await OpenColorAsync();
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<UnoMainViewModel>();
            return model;
        }
    }
}