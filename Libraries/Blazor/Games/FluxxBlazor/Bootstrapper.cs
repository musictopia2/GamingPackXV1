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
using FluxxCP.Cards;
using FluxxCP.Data;
using FluxxCP.ViewModels;
using System.Threading.Tasks;
namespace FluxxBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<FluxxShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<FluxxShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<FluxxPlayerItem, FluxxSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<FluxxPlayerItem, FluxxSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<FluxxPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<FluxxCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<FluxxCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, FluxxDeckCount>();
            return Task.CompletedTask;
        }
    }
}