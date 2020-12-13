using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
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
using HitTheDeckCP.Cards;
using HitTheDeckCP.Data;
using HitTheDeckCP.ViewModels;
using System.Threading.Tasks;
namespace HitTheDeckBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<HitTheDeckShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<HitTheDeckShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<HitTheDeckPlayerItem, HitTheDeckSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<HitTheDeckPlayerItem, HitTheDeckSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<HitTheDeckPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<HitTheDeckCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<HitTheDeckCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, HitTheDeckDeckCount>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            OurContainer.RegisterSingleton<IRegularDeckInfo, RegularAceHighSimpleDeck>();
            return Task.CompletedTask;
        }
    }
}