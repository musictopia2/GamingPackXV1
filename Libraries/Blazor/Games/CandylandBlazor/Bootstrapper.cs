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
using CandylandCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using CandylandCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.BasicDrawables.MiscClasses;
using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
//i think this is the most common things i like to do
namespace CandylandBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<CandylandShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<CandylandShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<CandylandPlayerItem, CandylandSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<CandylandPlayerItem, CandylandSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<CandylandPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DrawShuffleClass<CandylandCardData, CandylandPlayerItem>>(); //hopefully this does not have to be replaced.
            OurContainer.RegisterType<GenericCardShuffler<CandylandCardData>>(); //this is iffy too.
            OurContainer.RegisterSingleton<IDeckCount, CandylandCount>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}