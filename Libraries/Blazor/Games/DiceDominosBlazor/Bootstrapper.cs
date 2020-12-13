using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using DiceDominosCP.Data;
using DiceDominosCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace DiceDominosBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<DiceDominosShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<DiceDominosShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<DiceDominosPlayerItem, DiceDominosSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<DiceDominosPlayerItem, DiceDominosSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<DiceDominosPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, DiceDominosPlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();
            OurContainer.RegisterType<DominosBasicShuffler<SimpleDominoInfo>>();
            OurContainer.RegisterSingleton<IDeckCount, SimpleDominoInfo>(); //forgot this line.
            return Task.CompletedTask;
        }
    }
}