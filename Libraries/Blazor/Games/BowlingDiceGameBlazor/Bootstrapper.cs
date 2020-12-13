using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BowlingDiceGameCP.Data;
using BowlingDiceGameCP.ViewModels;
using System.Threading.Tasks;
namespace BowlingDiceGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BowlingDiceGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<BowlingDiceGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<BowlingDiceGamePlayerItem, BowlingDiceGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<BowlingDiceGamePlayerItem, BowlingDiceGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<BowlingDiceGamePlayerItem>>(true); //had to be set to true after all.
            return Task.CompletedTask;
        }
    }
}