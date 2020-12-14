using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ConnectFourCP.Data;
using ConnectFourCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ConnectFourBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ConnectFourShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ConnectFourShellViewModel>();

            OurContainer.RegisterCommonMultplayerClasses<ConnectFourPlayerItem, ConnectFourSaveInfo>();

            OurContainer.RegisterBeginningColors<EnumColorChoice, ConnectFourPlayerItem, ConnectFourSaveInfo>();

            return Task.CompletedTask;
        }
    }
}