using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using GoFishCP.Data;
using GoFishCP.ViewModels;
using System.Threading.Tasks;
namespace GoFishBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<GoFishShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<GoFishShellViewModel, RegularSimpleCard>();
            OurContainer!.RegisterType<BasicGameLoader<GoFishPlayerItem, GoFishSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<GoFishPlayerItem, GoFishSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<GoFishPlayerItem>>(true);
            return Task.CompletedTask;
        }
    }
}