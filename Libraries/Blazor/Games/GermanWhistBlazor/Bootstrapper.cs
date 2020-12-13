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
using GermanWhistCP.Cards;
using GermanWhistCP.Data;
using GermanWhistCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace GermanWhistBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<GermanWhistShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<GermanWhistShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<GermanWhistPlayerItem, GermanWhistSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<GermanWhistPlayerItem, GermanWhistSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<GermanWhistPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<GermanWhistCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<GermanWhistCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<GermanWhistCardInformation> sort = new SortSimpleCards<GermanWhistCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, GermanWhistCardInformation, GermanWhistPlayerItem, GermanWhistSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}