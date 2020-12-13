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
using Spades2PlayerCP.Cards;
using Spades2PlayerCP.Data;
using Spades2PlayerCP.ViewModels;
using System.Threading.Tasks;
namespace Spades2PlayerBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<Spades2PlayerShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<Spades2PlayerShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<Spades2PlayerPlayerItem, Spades2PlayerSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<Spades2PlayerPlayerItem, Spades2PlayerSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<Spades2PlayerPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<Spades2PlayerCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<Spades2PlayerCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<Spades2PlayerCardInformation> sort = new SortSimpleCards<Spades2PlayerCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, Spades2PlayerCardInformation, Spades2PlayerPlayerItem, Spades2PlayerSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}