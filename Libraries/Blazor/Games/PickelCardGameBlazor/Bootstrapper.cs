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
using PickelCardGameCP.Cards;
using PickelCardGameCP.Data;
using PickelCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace PickelCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<PickelCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<PickelCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<PickelCardGamePlayerItem, PickelCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<PickelCardGamePlayerItem, PickelCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<PickelCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<PickelCardGameCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<PickelCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<PickelCardGameCardInformation> sort = new SortSimpleCards<PickelCardGameCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, PickelCardGameCardInformation, PickelCardGamePlayerItem, PickelCardGameSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}