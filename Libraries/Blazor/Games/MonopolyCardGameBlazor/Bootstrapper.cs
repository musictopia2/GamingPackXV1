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
using MonopolyCardGameCP.Cards;
using MonopolyCardGameCP.Data;
using MonopolyCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace MonopolyCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<MonopolyCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<MonopolyCardGameShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<MonopolyCardGamePlayerItem, MonopolyCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<MonopolyCardGamePlayerItem, MonopolyCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<MonopolyCardGamePlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<MonopolyCardGameCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<MonopolyCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, MonopolyCardGameDeckCount>();
            return Task.CompletedTask;
        }
    }
}