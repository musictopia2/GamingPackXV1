using System;
using System.Text;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using fs = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.FileHelpers;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
using DominoBonesMultiplayerGamesCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using DominoBonesMultiplayerGamesCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
//i think this is the most common things i like to do
namespace DominoBonesMultiplayerGamesBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<DominoBonesMultiplayerGamesShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<DominoBonesMultiplayerGamesShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<DominoBonesMultiplayerGamesPlayerItem, DominoBonesMultiplayerGamesSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<DominoBonesMultiplayerGamesPlayerItem, DominoBonesMultiplayerGamesSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<DominoBonesMultiplayerGamesPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DominosBasicShuffler<SimpleDominoInfo>>(true);
            OurContainer.RegisterSingleton<IDeckCount, SimpleDominoInfo>(); //has to do this to stop overflow and duplicates bug.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}