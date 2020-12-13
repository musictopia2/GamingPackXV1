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
using SorryCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using SorryCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.BasicDrawables.MiscClasses;
using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
//i think this is the most common things i like to do
namespace SorryBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SorryShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SorryShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SorryPlayerItem, SorrySaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SorryPlayerItem, SorrySaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SorryPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, SorryPlayerItem, SorrySaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, SorryPlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, SorryPlayerItem>>();

            //good news is even if using a different item like pawn piece, won't affect this part (good decision).



            //i think this is still needed (?)
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                CustomBasicList<Type> output = new CustomBasicList<Type>()
                {
                    typeof(BeginningColorProcessorClass<EnumColorChoice, SorryPlayerItem, SorrySaveInfo>),
                    typeof(BeginningColorModel<EnumColorChoice, SorryPlayerItem>)
                    //typeof(BeginningColorProcessorClass<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, SorryPlayerItem, SorrySaveInfo>),
                    //typeof(BeginningColorModel<EnumColorChoice, CheckerChoiceCP<EnumColorChoice>, SorryPlayerItem>)
                };
                return output;
            });

            OurContainer.RegisterType<DrawShuffleClass<CardInfo, SorryPlayerItem>>();
            OurContainer.RegisterType<GenericCardShuffler<CardInfo>>();
            OurContainer.RegisterSingleton<IDeckCount, DeckCount>();

            return Task.CompletedTask;
        }
    }
}