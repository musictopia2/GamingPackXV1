using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Data;
using FiveCrownsCP.ViewModels;
using System.Linq;
using System.Threading.Tasks;
namespace FiveCrownsBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<FiveCrownsShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<FiveCrownsShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<FiveCrownsPlayerItem, FiveCrownsSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<FiveCrownsPlayerItem, FiveCrownsSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<FiveCrownsPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<FiveCrownsCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<FiveCrownsCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, FiveCrownsDeckCount>();
            return Task.CompletedTask;
        }
    }
}