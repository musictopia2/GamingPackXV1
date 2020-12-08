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
using BasicMultiplayerRegularCardGamesCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicMultiplayerRegularCardGamesCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
//i think this is the most common things i like to do
namespace BasicMultiplayerRegularCardGamesBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerRegularCardGamesShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<BasicMultiplayerRegularCardGamesShellViewModel, RegularSimpleCard>();
            OurContainer!.RegisterType<BasicGameLoader<BasicMultiplayerRegularCardGamesPlayerItem, BasicMultiplayerRegularCardGamesSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<BasicMultiplayerRegularCardGamesPlayerItem, BasicMultiplayerRegularCardGamesSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<BasicMultiplayerRegularCardGamesPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}