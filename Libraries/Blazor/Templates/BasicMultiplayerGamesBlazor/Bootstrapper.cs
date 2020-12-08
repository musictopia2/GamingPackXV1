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
using BasicMultiplayerGamesCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicMultiplayerGamesCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
//i think this is the most common things i like to do
namespace BasicMultiplayerGamesBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerGamesShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<BasicMultiplayerGamesShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<BasicMultiplayerGamesPlayerItem, BasicMultiplayerGamesSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<BasicMultiplayerGamesPlayerItem, BasicMultiplayerGamesSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<BasicMultiplayerGamesPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}