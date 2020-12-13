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
using DominosMexicanTrainCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using DominosMexicanTrainCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
//i think this is the most common things i like to do
namespace DominosMexicanTrainBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<DominosMexicanTrainShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        //human has to start.
        //there are some serious bugs with the train station now.
        //protected override Task RegisterTestsAsync()
        //{
        //    TestData!.AllowAnyMove = true; //to see what happens here.
        //    //TestData!.PlayCategory = BasicGameFrameworkLibrary.TestUtilities.EnumPlayCategory.NoShuffle;
        //    //TestData.WhoStarts = 2;
        //    return base.RegisterTestsAsync();
        //}
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<DominosMexicanTrainShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<DominosMexicanTrainPlayerItem, DominosMexicanTrainSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<DominosMexicanTrainPlayerItem, DominosMexicanTrainSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<DominosMexicanTrainPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DominosBasicShuffler<MexicanDomino>>(true);
            OurContainer.RegisterSingleton<IDeckCount, MexicanDomino>(); //has to do this to stop overflow and duplicates bug.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}