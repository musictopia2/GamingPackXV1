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
using Pinochle2PlayerCP.Cards;
using Pinochle2PlayerCP.Data;
using Pinochle2PlayerCP.ViewModels;
using System.Threading.Tasks;
namespace Pinochle2PlayerBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<Pinochle2PlayerShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<Pinochle2PlayerShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<Pinochle2PlayerPlayerItem, Pinochle2PlayerSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<Pinochle2PlayerPlayerItem, Pinochle2PlayerSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<Pinochle2PlayerPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<Pinochle2PlayerCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<Pinochle2PlayerCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<Pinochle2PlayerCardInformation> sort = new SortSimpleCards<Pinochle2PlayerCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, Pinochle2PlayerCardInformation, Pinochle2PlayerPlayerItem, Pinochle2PlayerSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}