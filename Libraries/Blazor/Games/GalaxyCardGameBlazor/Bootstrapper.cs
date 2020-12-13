using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using GalaxyCardGameCP.Cards;
using GalaxyCardGameCP.Data;
using GalaxyCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace GalaxyCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<GalaxyCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<GalaxyCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<GalaxyCardGamePlayerItem, GalaxyCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<GalaxyCardGamePlayerItem, GalaxyCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<GalaxyCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<GalaxyCardGameCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<GalaxyCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<GalaxyCardGameCardInformation> sort = new SortSimpleCards<GalaxyCardGameCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, GalaxyCardGameCardInformation, GalaxyCardGamePlayerItem, GalaxyCardGameSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}