using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using RackoCP.Cards;
using RackoCP.Data;
using RackoCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace RackoBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<RackoShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<RackoShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<RackoPlayerItem, RackoSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<RackoPlayerItem, RackoSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<RackoPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<RackoCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<RackoCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, RackoDeckCount>();
            return Task.CompletedTask;
        }
    }
}