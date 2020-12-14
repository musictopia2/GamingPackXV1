using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ConnectTheDotsCP.Data;
using ConnectTheDotsCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ConnectTheDotsBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ConnectTheDotsShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ConnectTheDotsShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>();
            return Task.CompletedTask;
        }
    }
}