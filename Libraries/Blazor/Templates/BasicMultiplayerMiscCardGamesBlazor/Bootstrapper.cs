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
using BasicMultiplayerMiscCardGamesCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicMultiplayerMiscCardGamesCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicMultiplayerMiscCardGamesCP.Cards;
using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
//i think this is the most common things i like to do
namespace BasicMultiplayerMiscCardGamesBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerMiscCardGamesShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<BasicMultiplayerMiscCardGamesShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<BasicMultiplayerMiscCardGamesPlayerItem, BasicMultiplayerMiscCardGamesSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<BasicMultiplayerMiscCardGamesPlayerItem, BasicMultiplayerMiscCardGamesSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<BasicMultiplayerMiscCardGamesPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DeckObservablePile<BasicMultiplayerMiscCardGamesCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<BasicMultiplayerMiscCardGamesCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, BasicMultiplayerMiscCardGamesDeckCount>();
            return Task.CompletedTask;
        }
    }
}