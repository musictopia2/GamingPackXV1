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
using FillOrBustCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using FillOrBustCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using FillOrBustCP.Cards;
using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.Dice;
//i think this is the most common things i like to do
namespace FillOrBustBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<FillOrBustShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<FillOrBustShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<FillOrBustPlayerItem, FillOrBustSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<FillOrBustPlayerItem, FillOrBustSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<FillOrBustPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DeckObservablePile<FillOrBustCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<FillOrBustCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, FillOrBustDeckCount>();
            OurContainer.RegisterSingleton<IGenerateDice<int>, SimpleDice>();
            OurContainer.RegisterType<StandardRollProcesses<SimpleDice, FillOrBustPlayerItem>>();
            return Task.CompletedTask;
        }
    }
}