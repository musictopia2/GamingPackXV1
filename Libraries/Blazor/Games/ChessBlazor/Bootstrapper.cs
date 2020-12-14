using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ChessCP.Data;
using ChessCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ChessBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ChessShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ChessShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<ChessPlayerItem, ChessSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, ChessPlayerItem, ChessSaveInfo>();
            return Task.CompletedTask;
        }
    }
}