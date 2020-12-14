using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CheckersCP.Data;
using CheckersCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CheckersBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<CheckersShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<CheckersShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<CheckersPlayerItem, CheckersSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, CheckersPlayerItem, CheckersSaveInfo>();
            return Task.CompletedTask;
        }
    }
}