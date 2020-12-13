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
using AggravationCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using AggravationCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace AggravationBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<AggravationShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        //protected override Task RegisterTestsAsync()
        //{
        //    TestData!.SaveOption = BasicGameFrameworkLibrary.TestUtilities.EnumTestSaveCategory.RestoreOnly; //to test the animations and fix.
        //    return base.RegisterTestsAsync();
        //}
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<AggravationShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<AggravationPlayerItem, AggravationSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<AggravationPlayerItem, AggravationSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<AggravationPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, AggravationPlayerItem, AggravationSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, AggravationPlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, AggravationPlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).

            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, AggravationPlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();


            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, AggravationPlayerItem, AggravationSaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, AggravationPlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, AggravationPlayerItem, AggravationSaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, AggravationPlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}