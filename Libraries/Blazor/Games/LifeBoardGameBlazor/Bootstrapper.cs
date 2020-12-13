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
using LifeBoardGameCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using LifeBoardGameCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
//i think this is the most common things i like to do
namespace LifeBoardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<LifeBoardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<LifeBoardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<LifeBoardGamePlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, LifeBoardGamePlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, LifeBoardGamePlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).



            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, LifeBoardGamePlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, LifeBoardGamePlayerItem, LifeBoardGameSaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, LifeBoardGamePlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}