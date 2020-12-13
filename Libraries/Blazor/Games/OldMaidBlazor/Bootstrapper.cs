using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using OldMaidCP.Data;
using OldMaidCP.ViewModels;
using System.Threading.Tasks;
namespace OldMaidBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<OldMaidShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<OldMaidShellViewModel, RegularSimpleCard>(customDeck: true);
            OurContainer!.RegisterType<BasicGameLoader<OldMaidPlayerItem, OldMaidSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<OldMaidPlayerItem, OldMaidSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<OldMaidPlayerItem>>(true);
            OurContainer.RegisterSingleton<IDeckCount, OldMaidDeck>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>();
            return Task.CompletedTask;
        }
    }
}