using AggravationCP.Data;
using AggravationCP.ViewModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace AggravationBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<AggravationShellViewModel>
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
            OurContainer!.RegisterNonSavedClasses<AggravationShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<AggravationPlayerItem, AggravationSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, AggravationPlayerItem, AggravationSaveInfo>(true);
            return Task.CompletedTask;
        }
    }
}