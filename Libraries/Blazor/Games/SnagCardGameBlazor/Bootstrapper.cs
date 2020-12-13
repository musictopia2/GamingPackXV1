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
using SnagCardGameCP.Cards;
using SnagCardGameCP.Data;
using SnagCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace SnagCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SnagCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SnagCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SnagCardGamePlayerItem, SnagCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SnagCardGamePlayerItem, SnagCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SnagCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<SnagCardGameCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<SnagCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<SnagCardGameCardInformation> sort = new SortSimpleCards<SnagCardGameCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            return Task.CompletedTask;
        }
    }
}