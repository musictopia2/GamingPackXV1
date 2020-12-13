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
using RoundsCardGameCP.Cards;
using RoundsCardGameCP.Data;
using RoundsCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace RoundsCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<RoundsCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<RoundsCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<RoundsCardGamePlayerItem, RoundsCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<RoundsCardGamePlayerItem, RoundsCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<RoundsCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<RoundsCardGameCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<RoundsCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<RoundsCardGameCardInformation> sort = new SortSimpleCards<RoundsCardGameCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, RoundsCardGameCardInformation, RoundsCardGamePlayerItem, RoundsCardGameSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}