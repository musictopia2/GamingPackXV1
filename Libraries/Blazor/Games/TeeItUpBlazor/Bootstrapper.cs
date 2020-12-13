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
using TeeItUpCP.Cards;
using TeeItUpCP.Data;
using TeeItUpCP.ViewModels;
namespace TeeItUpBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<TeeItUpShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<TeeItUpShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<TeeItUpPlayerItem, TeeItUpSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<TeeItUpPlayerItem, TeeItUpSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<TeeItUpPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<TeeItUpCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<TeeItUpCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, TeeItUpDeckCount>();
            return Task.CompletedTask;
        }
    }
}