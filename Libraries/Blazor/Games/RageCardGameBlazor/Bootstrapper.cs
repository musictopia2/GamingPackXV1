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
using RageCardGameCP.Cards;
using RageCardGameCP.Data;
using RageCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace RageCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<RageCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<RageCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<RageCardGamePlayerItem, RageCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<RageCardGamePlayerItem, RageCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<RageCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<RageCardGameCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<RageCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, RageCardGameDeckCount>();
            return Task.CompletedTask;
        }
    }
}