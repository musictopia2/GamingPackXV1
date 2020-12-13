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
using CaliforniaJackCP.Cards;
using CaliforniaJackCP.Data;
using CaliforniaJackCP.ViewModels;
using System.Threading.Tasks;
namespace CaliforniaJackBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<CaliforniaJackShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<CaliforniaJackShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<CaliforniaJackPlayerItem, CaliforniaJackSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<CaliforniaJackPlayerItem, CaliforniaJackSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<CaliforniaJackPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<CaliforniaJackCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<CaliforniaJackCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<CaliforniaJackCardInformation> sort = new SortSimpleCards<CaliforniaJackCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, CaliforniaJackCardInformation, CaliforniaJackPlayerItem, CaliforniaJackSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}