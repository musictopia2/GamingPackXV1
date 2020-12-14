using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace LifeBoardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<LifeBoardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<LifeBoardGameShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>();


            return Task.CompletedTask;
        }
    }
}