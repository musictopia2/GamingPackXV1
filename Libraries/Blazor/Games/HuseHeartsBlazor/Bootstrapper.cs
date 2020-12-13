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
using HuseHeartsCP.Cards;
using HuseHeartsCP.Data;
using HuseHeartsCP.ViewModels;
using System.Threading.Tasks;
namespace HuseHeartsBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<HuseHeartsShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<HuseHeartsShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<HuseHeartsPlayerItem, HuseHeartsSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<HuseHeartsPlayerItem, HuseHeartsSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<HuseHeartsPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<HuseHeartsCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<HuseHeartsCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<HuseHeartsCardInformation> sort = new SortSimpleCards<HuseHeartsCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            return Task.CompletedTask;
        }
    }
}