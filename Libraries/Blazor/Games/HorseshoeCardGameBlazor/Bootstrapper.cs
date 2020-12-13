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
using HorseshoeCardGameCP.Cards;
using HorseshoeCardGameCP.Data;
using HorseshoeCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace HorseshoeCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<HorseshoeCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<HorseshoeCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<HorseshoeCardGamePlayerItem, HorseshoeCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<HorseshoeCardGamePlayerItem, HorseshoeCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<HorseshoeCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<HorseshoeCardGameCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<HorseshoeCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<HorseshoeCardGameCardInformation> sort = new SortSimpleCards<HorseshoeCardGameCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            return Task.CompletedTask;
        }
    }
}