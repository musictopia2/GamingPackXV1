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
using FlinchCP.Cards;
using FlinchCP.Data;
using FlinchCP.ViewModels;
using System.Threading.Tasks;
namespace FlinchBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<FlinchShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<FlinchShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<FlinchPlayerItem, FlinchSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<FlinchPlayerItem, FlinchSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<FlinchPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<FlinchCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<FlinchCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, FlinchDeckCount>();
            return Task.CompletedTask;
        }
    }
}