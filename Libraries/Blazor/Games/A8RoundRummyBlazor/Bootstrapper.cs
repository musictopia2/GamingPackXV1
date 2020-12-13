using A8RoundRummyCP.Cards;
using A8RoundRummyCP.Data;
using A8RoundRummyCP.ViewModels;
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
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace A8RoundRummyBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<A8RoundRummyShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<A8RoundRummyShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<A8RoundRummyPlayerItem, A8RoundRummySaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<A8RoundRummyPlayerItem, A8RoundRummySaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<A8RoundRummyPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DeckObservablePile<A8RoundRummyCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<A8RoundRummyCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, A8RoundRummyDeckCount>();
            return Task.CompletedTask;
        }
    }
}