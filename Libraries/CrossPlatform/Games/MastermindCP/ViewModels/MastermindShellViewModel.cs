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
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
//i think this is the most common things i like to do
namespace MastermindCP.ViewModels
{
    public class MastermindShellViewModel : SinglePlayerShellViewModel
    {

        public MastermindShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override bool AlwaysNewGame => false; //most games allow new game always.
        protected override bool AutoStartNewGame => false;

        public IBlazorScreen? OpeningScreen { get; set; }
        public IBlazorScreen? SolutionScreen { get; set; }

        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<MastermindMainViewModel>();
            return model;
        }
        protected override async Task NewGameRequestedAsync()
        {
            if (OpeningScreen == null)
            {
                throw new BasicBlankException("There was no opening screen.  Rethink");
            }
            if (SolutionScreen != null)
            {
                await CloseSpecificChildAsync(SolutionScreen);
                SolutionScreen = null;
            }
            await CloseSpecificChildAsync(OpeningScreen);
        }
        protected override Task GameOverScreenAsync()
        {
            SolutionScreen = MainContainer.Resolve<SolutionViewModel>();
            return LoadScreenAsync(SolutionScreen);
        }
        protected override async Task OpenStartingScreensAsync()
        {
            OpeningScreen = MainContainer.Resolve<MastermindOpeningViewModel>(); //i think has to be this way so its fresh everytime.
            await LoadScreenAsync(OpeningScreen); //try this way.
            await ShowNewGameAsync();
            FinishInit();
        }
    }
}
