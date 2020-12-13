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
using SnakesAndLaddersCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using SnakesAndLaddersCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace SnakesAndLaddersBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SnakesAndLaddersShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        //protected override Task RegisterTestsAsync()
        //{
        //    TestData!.AllowAnyMove = true;
        //    TestData.SaveOption = BasicGameFrameworkLibrary.TestUtilities.EnumTestSaveCategory.RestoreOnly;
        //    return base.RegisterTestsAsync();
        //}
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SnakesAndLaddersShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SnakesAndLaddersPlayerItem, SnakesAndLaddersSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SnakesAndLaddersPlayerItem, SnakesAndLaddersSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SnakesAndLaddersPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();
            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, SnakesAndLaddersPlayerItem>>(); //iffy.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}