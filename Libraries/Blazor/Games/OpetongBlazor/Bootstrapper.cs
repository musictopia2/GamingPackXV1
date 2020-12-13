using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using OpetongCP.Data;
using OpetongCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
//i think this is the most common things i like to do
namespace OpetongBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<OpetongShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {

            OurContainer!.RegisterType<BasicGameLoader<OpetongPlayerItem, OpetongSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<OpetongPlayerItem, OpetongSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<OpetongPlayerItem>>(true); //had to be set to true after all.

            OurContainer!.RegisterNonSavedClasses<OpetongMainViewModel>();
            OurContainer.RegisterType<RegularCardsBasicShuffler<RegularRummyCard>>(true);
            OurContainer.RegisterType<DeckObservablePile<RegularRummyCard>>(true); //i think
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory ThisCat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<RegularRummyCard> ThisSort = new SortSimpleCards<RegularRummyCard>();
                ThisSort.SuitForSorting = ThisCat.SortCategory;
                OurContainer.RegisterSingleton(ThisSort); //if we have a custom one, will already be picked up.
            }
            OurContainer.RegisterSingleton<IDeckCount, RegularAceLowSimpleDeck>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>();
            return Task.CompletedTask;
        }
    }
}