using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using SequenceDiceCP.Data;
using SequenceDiceCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace SequenceDiceBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SequenceDiceShellViewModel>
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
            OurContainer!.RegisterNonSavedClasses<SequenceDiceShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<SequenceDicePlayerItem, SequenceDiceSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, SequenceDicePlayerItem, SequenceDiceSaveInfo>(true);
            return Task.CompletedTask;
        }
    }
}