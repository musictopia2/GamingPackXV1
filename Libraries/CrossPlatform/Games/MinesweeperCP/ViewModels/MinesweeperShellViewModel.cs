using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace MinesweeperCP.ViewModels
{
    public class MinesweeperShellViewModel : SinglePlayerShellViewModel
    {

        public MinesweeperShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }

        protected override bool AlwaysNewGame => false;
        protected override bool AutoStartNewGame => false;

        public IBlazorScreen? OpeningScreen { get; set; }

        protected override async Task OpenStartingScreensAsync()
        {
            OpeningScreen = MainContainer.Resolve<MinesweeperOpeningViewModel>(); //i think has to be this way so its fresh everytime.
            await LoadScreenAsync(OpeningScreen); //try this way.
            await ShowNewGameAsync();
            FinishInit();
        }

        protected override Task NewGameRequestedAsync()
        {
            if (OpeningScreen == null)
            {
                throw new BasicBlankException("There was no opening screen.  Rethink");
            }
            return CloseSpecificChildAsync(OpeningScreen);
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<MinesweeperMainViewModel>();
            return model;
        }
    }
}