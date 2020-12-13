using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks;
using YahtzeeHandsDownCP.Cards;
using YahtzeeHandsDownCP.Data;
using YahtzeeHandsDownCP.ViewModels;
namespace YahtzeeHandsDownBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<YahtzeeHandsDownShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<YahtzeeHandsDownShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<YahtzeeHandsDownPlayerItem, YahtzeeHandsDownSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<YahtzeeHandsDownPlayerItem, YahtzeeHandsDownSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<YahtzeeHandsDownPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<YahtzeeHandsDownCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<YahtzeeHandsDownCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, YahtzeeHandsDownDeckCount>();
            return Task.CompletedTask;
        }
    }
}