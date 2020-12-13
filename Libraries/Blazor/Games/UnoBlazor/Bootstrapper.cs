using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks; 
using UnoCP.Cards;
using UnoCP.Data;
using UnoCP.ViewModels;
namespace UnoBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<UnoShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<UnoShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<UnoPlayerItem, UnoSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<UnoPlayerItem, UnoSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<UnoPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<UnoCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<UnoCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, Phase10UnoDeck>();
            return Task.CompletedTask;
        }
    }
}