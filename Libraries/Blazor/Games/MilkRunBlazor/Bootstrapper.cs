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
using MilkRunCP.Cards;
using MilkRunCP.Data;
using MilkRunCP.ViewModels;
using System.Threading.Tasks;
namespace MilkRunBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<MilkRunShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<MilkRunShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<MilkRunPlayerItem, MilkRunSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<MilkRunPlayerItem, MilkRunSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<MilkRunPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<MilkRunCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<MilkRunCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, MilkRunDeckCount>();
            return Task.CompletedTask;
        }
    }
}