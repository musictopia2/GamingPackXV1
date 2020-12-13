using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CountdownCP.Data;
using CountdownCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace CountdownBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<CountdownShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<CountdownShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<CountdownPlayerItem, CountdownSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<CountdownPlayerItem, CountdownSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<CountdownPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<StandardRollProcesses<CountdownDice, CountdownPlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, CountdownDice>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}