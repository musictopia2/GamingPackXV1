using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using ThreeLetterFunCP.Data;
using ThreeLetterFunCP.ViewModels;
namespace ThreeLetterFunBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ThreeLetterFunShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ThreeLetterFunShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<ThreeLetterFunPlayerItem, ThreeLetterFunSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<ThreeLetterFunPlayerItem, ThreeLetterFunSaveInfo>>();
            OurContainer.RegisterType<GenericCardShuffler<ThreeLetterFunCardData>>();
            OurContainer.RegisterSingleton<IDeckCount, ThreeLetterFunDeckInfo>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<ThreeLetterFunPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}