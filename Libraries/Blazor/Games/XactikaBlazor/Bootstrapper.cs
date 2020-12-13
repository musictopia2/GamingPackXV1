using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks;
using XactikaCP.Cards;
using XactikaCP.Data;
using XactikaCP.ViewModels;
namespace XactikaBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<XactikaShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<XactikaShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<XactikaPlayerItem, XactikaSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<XactikaPlayerItem, XactikaSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<XactikaPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<XactikaCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<XactikaCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, XactikaDeckCount>();
            OurContainer.RegisterType<SeveralPlayersTrickObservable<EnumShapes, XactikaCardInformation, XactikaPlayerItem, XactikaSaveInfo>>();
            return Task.CompletedTask;
        }
    }
}