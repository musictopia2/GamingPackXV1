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
using SkuckCardGameCP.Cards;
using SkuckCardGameCP.Data;
using SkuckCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace SkuckCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SkuckCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SkuckCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SkuckCardGamePlayerItem, SkuckCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SkuckCardGamePlayerItem, SkuckCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SkuckCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<SkuckCardGameCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<SkuckCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<SkuckCardGameCardInformation> sort = new SortSimpleCards<SkuckCardGameCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, SkuckCardGameCardInformation, SkuckCardGamePlayerItem, SkuckCardGameSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}