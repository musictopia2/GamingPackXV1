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
using PassOutDiceGameCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using PassOutDiceGameCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace PassOutDiceGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<PassOutDiceGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<PassOutDiceGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<PassOutDiceGamePlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, PassOutDiceGamePlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, PassOutDiceGamePlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).

            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, PassOutDiceGamePlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();


            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, PassOutDiceGamePlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, PassOutDiceGamePlayerItem, PassOutDiceGameSaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, PassOutDiceGamePlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}