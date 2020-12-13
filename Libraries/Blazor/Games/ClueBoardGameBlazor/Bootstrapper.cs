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
using ClueBoardGameCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using ClueBoardGameCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace ClueBoardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ClueBoardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        //protected override Task RegisterTestsAsync()
        //{
        //    TestData!.AllowAnyMove = true;
        //    return base.RegisterTestsAsync();
        //}

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ClueBoardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<ClueBoardGamePlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, ClueBoardGamePlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, ClueBoardGamePlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).

            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, ClueBoardGamePlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();


            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, ClueBoardGamePlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, ClueBoardGamePlayerItem, ClueBoardGameSaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, ClueBoardGamePlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}