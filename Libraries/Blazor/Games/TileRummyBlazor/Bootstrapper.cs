using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using TileRummyCP.Data;
using TileRummyCP.Logic;
using TileRummyCP.ViewModels;
namespace TileRummyBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<TileRummyShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<TileRummyShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<TileRummyPlayerItem, TileRummySaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<TileRummyPlayerItem, TileRummySaveInfo>>();
            OurContainer.RegisterType<TileShuffler>();
            OurContainer.RegisterSingleton<IDeckCount, TileCountClass>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<TileRummyPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}