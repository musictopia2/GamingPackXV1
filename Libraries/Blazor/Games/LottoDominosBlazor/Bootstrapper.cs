using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using LottoDominosCP.Data;
using LottoDominosCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace LottoDominosBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<LottoDominosShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<LottoDominosShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<LottoDominosPlayerItem, LottoDominosSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<LottoDominosPlayerItem, LottoDominosSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<LottoDominosPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterSingleton<IDeckCount, SimpleDominoInfo>(); //hopefully this is all i need (?)
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}