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
using BasicMultiplayerTrickCardGamesCP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicMultiplayerTrickCardGamesCP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicMultiplayerTrickCardGamesCP.Cards;
using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
//i think this is the most common things i like to do
namespace BasicMultiplayerTrickCardGamesBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerTrickCardGamesShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<BasicMultiplayerTrickCardGamesShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<BasicMultiplayerTrickCardGamesPlayerItem, BasicMultiplayerTrickCardGamesSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<BasicMultiplayerTrickCardGamesPlayerItem, BasicMultiplayerTrickCardGamesSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<BasicMultiplayerTrickCardGamesPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DeckObservablePile<BasicMultiplayerTrickCardGamesCardInformation>>(true);

            //usually for regular deck of cards.  if that changes, then change.

            OurContainer.RegisterType<RegularCardsBasicShuffler<BasicMultiplayerTrickCardGamesCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>(); //most of the time, ace is high for trick taking games.

            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<ISortCategory>();
            if (rets == true)
            {
                ISortCategory cat = OurContainer.Resolve<ISortCategory>();
                SortSimpleCards<BasicMultiplayerTrickCardGamesCardInformation> sort = new SortSimpleCards<BasicMultiplayerTrickCardGamesCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort); //if we have a custom one, will already be picked up.
            }
            //change view model for area if not using 2 player.
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, BasicMultiplayerTrickCardGamesCardInformation, BasicMultiplayerTrickCardGamesPlayerItem, BasicMultiplayerTrickCardGamesSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}