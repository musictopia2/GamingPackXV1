using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace FroggiesCP.ViewModels
{
    public class FroggiesShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => false; //most games allow new game always.

        protected override bool AutoStartNewGame => false;

        public IBlazorScreen? OpeningScreen { get; set; }

        public FroggiesShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }

        protected override Task GameOverScreenAsync()
        {
            throw new BasicBlankException("There is no option for new game.  Rethink");
        }

        protected override async Task NewGameRequestedAsync()
        {
            if (OpeningScreen == null)
            {
                throw new BasicBlankException("There was no opening screen.  Rethink");
            }
            //EmulateCloseOpen.Invoke();
            await CloseSpecificChildAsync(OpeningScreen);
            OpeningScreen = null;
        }

        protected override async Task OpenStartingScreensAsync()
        {
            OpeningScreen = MainContainer.Resolve<FroggiesOpeningViewModel>(); //i think has to be this way so its fresh everytime.
            await LoadScreenAsync(OpeningScreen); //try this way.
            await ShowNewGameAsync();
            FinishInit();
        }

        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<FroggiesMainViewModel>();
            return model;
        }
    }
}
