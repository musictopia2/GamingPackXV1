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
using Phase10CP.Cards;
using Phase10CP.Data;
using Phase10CP.ViewModels;
using System.Threading.Tasks;
namespace Phase10Blazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<Phase10ShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<Phase10ShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<Phase10PlayerItem, Phase10SaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<Phase10PlayerItem, Phase10SaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<Phase10PlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterType<DeckObservablePile<Phase10CardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<Phase10CardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, Phase10UnoDeck>();
            return Task.CompletedTask;
        }
    }
}