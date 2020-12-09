using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
using BuncoDiceGameCP.EventModels;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BuncoDiceGameCP.ViewModels
{
    public class BuncoDiceGameShellViewModel : SinglePlayerShellViewModel,
        IHandleAsync<ChoseNewRoundEventModel>,
        IHandleAsync<EndGameEventModel>,
        IHandleAsync<NewRoundEventModel>
    {

        public BuncoDiceGameShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override bool AlwaysNewGame => false; //most games allow new game always.
        protected override bool AutoStartNewGame => true;
        public IBlazorScreen? TempScreen { get; set; } //iffy.


        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<BuncoDiceGameMainViewModel>();
            return model;
        }

        async Task IHandleAsync<ChoseNewRoundEventModel>.HandleAsync(ChoseNewRoundEventModel message)
        {
            if (TempScreen == null)
            {
                throw new BasicBlankException("No screen was set up to show you chose new round.  Rethink");
            }
            await CloseSpecificChildAsync(TempScreen);
            TempScreen = null;
        }

        Task IHandleAsync<EndGameEventModel>.HandleAsync(EndGameEventModel message)
        {
            if (TempScreen != null)
            {
                throw new BasicBlankException("The screen was never closed out.  Rethink");
            }
            TempScreen = MainContainer.Resolve<EndGameViewModel>();
            return LoadScreenAsync(TempScreen);
        }
        protected override async Task GameOverScreenAsync()
        {
            if (TempScreen == null)
            {
                throw new BasicBlankException("Must have the end screen first.  Rethink");
            }
            await CloseSpecificChildAsync(TempScreen);
            TempScreen = null;
        }
        Task IHandleAsync<NewRoundEventModel>.HandleAsync(NewRoundEventModel message)
        {
            if (TempScreen != null)
            {
                throw new BasicBlankException("The screen was never closed out.  Rethink");
            }
            TempScreen = MainContainer.Resolve<BuncoNewRoundViewModel>();
            return LoadScreenAsync(TempScreen);
        }
    }
}
