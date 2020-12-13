using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using RummyDiceCP.Data;
using RummyDiceCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace RummyDiceBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<RummyDiceShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<RummyDiceShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<RummyDicePlayerItem, RummyDiceSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<RummyDicePlayerItem, RummyDiceSaveInfo>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, RummyDiceInfo>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<RummyDicePlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}