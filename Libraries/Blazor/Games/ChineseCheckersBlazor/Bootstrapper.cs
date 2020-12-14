using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ChineseCheckersCP.Data;
using ChineseCheckersCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ChineseCheckersBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ChineseCheckersShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ChineseCheckersShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<ChineseCheckersPlayerItem, ChineseCheckersSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, ChineseCheckersPlayerItem, ChineseCheckersSaveInfo>();

            return Task.CompletedTask;
        }
    }
}