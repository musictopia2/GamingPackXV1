using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ViewModels
{
    public abstract class SinglePlayerShellViewModel : BlazorConductorViewModel, IHandleAsync<NewGameEventModel>,
        IHandleAsync<GameOverEventModel>,
        IMainGPXShellVM
    {
        private readonly ISaveSinglePlayerClass _saves;
        public SinglePlayerShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            ISaveSinglePlayerClass saves
            )
        {
            MainContainer = mainContainer; //the subscribe part is already done for me too.
            CommandContainer = container;
            GameData = gameData;
            _saves = saves;
            DisplayName = gameData.GameName;
        }

        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            if (AlwaysNewGame)
            {
                await ShowNewGameAsync();
            }
            if (AutoStartNewGame)
            {
                await StartNewGameAsync();
            }
            else
            {
                await OpenStartingScreensAsync();
            }
        }
        /// <summary>
        /// this should be responsible for any opening screens that is not new game.  new game is automatic.
        /// if we find a case where it can't be automatic, rethink.
        /// </summary>
        /// <returns></returns>
        protected virtual Task OpenStartingScreensAsync() => Task.CompletedTask;

        private async Task StartNewGameAsync()
        {
            CommandContainer.ClearLists(); //try this too.  hopefully this simple.
            MainVM = GetMainViewModel();
            await LoadScreenAsync(MainVM);
            FinishInit();
        }
        protected void FinishInit()
        {
            CommandContainer.Processing = false;
            CommandContainer.IsExecuting = false;
        }

        protected async Task ShowNewGameAsync()
        {
            NewGameVM = MainContainer.Resolve<INewGameVM>("");
            await LoadScreenAsync(NewGameVM);
        }

        /// <summary>
        /// this is needed because it may need to resolve other things to load other things but not at the beginning.
        /// </summary>
        public IGamePackageResolver MainContainer { get; }
        protected CommandContainer CommandContainer { get; }
        protected IGameInfo GameData { get; }
        public INewGameVM? NewGameVM { get; set; } //this is one for the ui to know

        public IMainScreen? MainVM { get; set; } //this is another one for the ui to know.

        /// <summary>
        /// this is the view model that represents the body.  its used when you decide on new game.
        /// </summary>
        /// <returns></returns>
        protected abstract IMainScreen GetMainViewModel();


        protected abstract bool AlwaysNewGame { get; }

        /// <summary>
        /// usually can automatically start a new game upon loading.
        /// however some games requires settings to be chosen first.
        /// 
        /// </summary>
        protected virtual bool AutoStartNewGame => true;

        async Task IHandleAsync<NewGameEventModel>.HandleAsync(NewGameEventModel message)
        {
            if (NewGameVM == null)
            {
                throw new BasicBlankException("New game was not even active.  Therefore, I should not have received message for requesting new game");
            }
            if (AlwaysNewGame == false)
            {
                await CloseSpecificChildAsync(NewGameVM);
                NewGameVM = null;//forgot to set to null.
            }
            if (MainVM != null)
            {
                await CloseSpecificChildAsync(MainVM);
            }
            MainVM = null; //looks like i have to set to null manually.
            await _saves.DeleteSinglePlayerGameAsync(); //i think.
            await NewGameRequestedAsync();
            await StartNewGameAsync();
        }
        /// <summary>
        /// this is used in cases where somebody actually clicks new game but sometimes extra screens has to be closed out.
        /// i could decide later to have a list it keeps track of to close out.
        /// </summary>
        /// <returns></returns>
        protected virtual Task NewGameRequestedAsync() => Task.CompletedTask;
        /// <summary>
        /// this is used in cases like mastermind where you need to show a solution when its game over.
        /// </summary>
        /// <returns></returns>
        protected virtual Task GameOverScreenAsync() => Task.CompletedTask;

        async Task IHandleAsync<GameOverEventModel>.HandleAsync(GameOverEventModel message)
        {
            CommandContainer.ClearLists(); //try this too.
            if (MainVM == null)
            {
                throw new BasicBlankException("The main view model was not even available.  Rethink");
            }
            await _saves.DeleteSinglePlayerGameAsync(); //just in case its forgotten, it will be deleted.
            await CloseSpecificChildAsync(MainVM);
            MainVM = null;
            if (NewGameVM != null)
            {
                return;
            }
            await GameOverScreenAsync();
            if (AutoStartNewGame == false)
            {
                await OpenStartingScreensAsync();
            }
            else
            {
                await ShowNewGameAsync(); //try this way (?)
            }
        }
    }
}