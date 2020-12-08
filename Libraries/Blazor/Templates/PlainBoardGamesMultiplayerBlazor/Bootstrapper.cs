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
using PlainBoardGamesMultiplayerCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using PlainBoardGamesMultiplayerCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
//i think this is the most common things i like to do
namespace PlainBoardGamesMultiplayerBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<PlainBoardGamesMultiplayerShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<PlainBoardGamesMultiplayerShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<PlainBoardGamesMultiplayerPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).



            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, PlainBoardGamesMultiplayerPlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}