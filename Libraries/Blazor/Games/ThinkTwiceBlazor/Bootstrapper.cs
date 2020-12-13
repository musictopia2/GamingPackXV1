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
using ThinkTwiceCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using ThinkTwiceCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace ThinkTwiceBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ThinkTwiceShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ThinkTwiceShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<ThinkTwicePlayerItem, ThinkTwiceSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<ThinkTwicePlayerItem, ThinkTwiceSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<ThinkTwicePlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, ThinkTwicePlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}