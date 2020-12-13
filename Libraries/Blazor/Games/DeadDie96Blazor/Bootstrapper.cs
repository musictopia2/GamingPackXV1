using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using DeadDie96CP.Data;
using DeadDie96CP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace DeadDie96Blazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<DeadDie96ShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<DeadDie96ShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<DeadDie96PlayerItem, DeadDie96SaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<DeadDie96PlayerItem, DeadDie96SaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<DeadDie96PlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<StandardRollProcesses<TenSidedDice, DeadDie96PlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, TenSidedDice>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}