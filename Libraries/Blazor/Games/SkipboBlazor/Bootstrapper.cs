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
using SkipboCP.Cards;
using SkipboCP.Data;
using SkipboCP.ViewModels;
using System.Threading.Tasks;
namespace SkipboBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SkipboShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        //protected override Task RegisterTestsAsync()
        //{
        //    TestData.SaveOption = BasicGameFrameworkLibrary.TestUtilities.EnumTestSaveCategory.RestoreOnly;
        //    return base.RegisterTestsAsync();
        //}
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SkipboShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<SkipboPlayerItem, SkipboSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<SkipboPlayerItem, SkipboSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<SkipboPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<SkipboCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<SkipboCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, SkipboDeckCount>();
            return Task.CompletedTask;
        }
    }
}