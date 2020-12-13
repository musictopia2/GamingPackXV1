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
using PaydayCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using PaydayCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace PaydayBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<PaydayShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<PaydayShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<PaydayPlayerItem, PaydaySaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<PaydayPlayerItem, PaydaySaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<PaydayPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, PaydayPlayerItem, PaydaySaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, PaydayPlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, PaydayPlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).

            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, PaydayPlayerItem>>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();


            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, PaydayPlayerItem, PaydaySaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, PaydayPlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, PaydayPlayerItem, PaydaySaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, PaydayPlayerItem>)
                };
                return output;
            });

            return Task.CompletedTask;
        }
    }
}