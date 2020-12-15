using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using PassOutDiceGameCP.Data;
using PassOutDiceGameCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace PassOutDiceGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<PassOutDiceGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task RegisterTestsAsync()
        {
            TestData!.ImmediatelyEndGame = true;
            return base.RegisterTestsAsync();
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<PassOutDiceGameShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>();
            return Task.CompletedTask;
        }
    }
}