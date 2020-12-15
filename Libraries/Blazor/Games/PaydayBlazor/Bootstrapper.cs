using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using PaydayCP.Data;
using PaydayCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace PaydayBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<PaydayShellViewModel>
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
            OurContainer!.RegisterNonSavedClasses<PaydayShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<PaydayPlayerItem, PaydaySaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, PaydayPlayerItem, PaydaySaveInfo>(true);
            return Task.CompletedTask;
        }
    }
}