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
using RookCP.Cards;
using RookCP.Data;
using RookCP.ViewModels;
using System.Threading.Tasks;
namespace RookBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<RookShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<RookShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<RookPlayerItem, RookSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<RookPlayerItem, RookSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<RookPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<RookCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<RookCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, RookDeckCount>();
            return Task.CompletedTask;
        }
    }
}