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
using SixtySix2PlayerCP.Cards;
using SixtySix2PlayerCP.Data;
using SixtySix2PlayerCP.ViewModels;
using System.Threading.Tasks;
namespace SixtySix2PlayerBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SixtySix2PlayerShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SixtySix2PlayerShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SixtySix2PlayerPlayerItem, SixtySix2PlayerSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SixtySix2PlayerPlayerItem, SixtySix2PlayerSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SixtySix2PlayerPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<SixtySix2PlayerCardInformation>>(true);
            OurContainer.RegisterType<RegularCardsBasicShuffler<SixtySix2PlayerCardInformation>>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>();
            bool rets = OurContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory cat = OurContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<SixtySix2PlayerCardInformation> sort = new SortSimpleCards<SixtySix2PlayerCardInformation>();
                sort.SuitForSorting = cat.SortCategory;
                OurContainer.RegisterSingleton(sort);
            }
            OurContainer.RegisterType<TwoPlayerTrickObservable<EnumSuitList, SixtySix2PlayerCardInformation, SixtySix2PlayerPlayerItem, SixtySix2PlayerSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}