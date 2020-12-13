using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CousinRummyCP.Data;
using CousinRummyCP.Logic;
using CousinRummyCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CousinRummyBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<CousinRummyShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<CousinRummyShellViewModel, RegularRummyCard>(customDeck: true);
            OurContainer!.RegisterType<BasicGameLoader<CousinRummyPlayerItem, CousinRummySaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<CousinRummyPlayerItem, CousinRummySaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<CousinRummyPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
            return Task.CompletedTask;
        }
    }
}