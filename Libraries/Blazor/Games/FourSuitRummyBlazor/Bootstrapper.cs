using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using FourSuitRummyCP.Data;
using FourSuitRummyCP.ViewModels;
using System.Threading.Tasks;
namespace FourSuitRummyBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<FourSuitRummyShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<FourSuitRummyShellViewModel, RegularRummyCard>();
            OurContainer!.RegisterType<BasicGameLoader<FourSuitRummyPlayerItem, FourSuitRummySaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<FourSuitRummyPlayerItem, FourSuitRummySaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<FourSuitRummyPlayerItem>>(true); //had to be set to true after all.
            return Task.CompletedTask;
        }
    }
}