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
using DiceBoardGamesMultiplayerCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using DiceBoardGamesMultiplayerCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace DiceBoardGamesMultiplayerBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<DiceBoardGamesMultiplayerShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<DiceBoardGamesMultiplayerShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<DiceBoardGamesMultiplayerPlayerItem, DiceBoardGamesMultiplayerSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<DiceBoardGamesMultiplayerPlayerItem, DiceBoardGamesMultiplayerSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<DiceBoardGamesMultiplayerPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, DiceBoardGamesMultiplayerPlayerItem, DiceBoardGamesMultiplayerSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, DiceBoardGamesMultiplayerPlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, DiceBoardGamesMultiplayerPlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).

            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, DiceBoardGamesMultiplayerPlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();


            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, DiceBoardGamesMultiplayerPlayerItem, DiceBoardGamesMultiplayerSaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, DiceBoardGamesMultiplayerPlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, DiceBoardGamesMultiplayerPlayerItem, DiceBoardGamesMultiplayerSaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, DiceBoardGamesMultiplayerPlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}