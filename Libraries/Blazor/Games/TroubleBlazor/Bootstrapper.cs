using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using TroubleCP.Data;
using TroubleCP.ViewModels;
namespace TroubleBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<TroubleShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<TroubleShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<TroublePlayerItem, TroubleSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, TroublePlayerItem, TroubleSaveInfo>(true);
            return Task.CompletedTask;
        }
    }
}