using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CrazyEightsCP.Data;
using CrazyEightsCP.ViewModels;
using System.Threading.Tasks;
namespace CrazyEightsBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<CrazyEightsShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<CrazyEightsShellViewModel, RegularSimpleCard>();
            OurContainer!.RegisterType<BasicGameLoader<CrazyEightsPlayerItem, CrazyEightsSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<CrazyEightsPlayerItem, CrazyEightsSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<CrazyEightsPlayerItem>>(true); //had to be set to true after all.
            return Task.CompletedTask;
        }
    }
}