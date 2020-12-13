using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using SinisterSixCP.Data;
using SinisterSixCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace SinisterSixBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SinisterSixShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SinisterSixShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SinisterSixPlayerItem, SinisterSixSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SinisterSixPlayerItem, SinisterSixSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SinisterSixPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<StandardRollProcesses<EightSidedDice, SinisterSixPlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, EightSidedDice>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}