using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ClueBoardGameCP.Data;
using ClueBoardGameCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ClueBoardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ClueBoardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ClueBoardGameShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>();
            return Task.CompletedTask;
        }
    }
}