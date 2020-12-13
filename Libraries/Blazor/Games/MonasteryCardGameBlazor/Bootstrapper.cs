using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using MonasteryCardGameCP.Data;
using MonasteryCardGameCP.Logic;
using MonasteryCardGameCP.ViewModels;
using System.Threading.Tasks;
namespace MonasteryCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<MonasteryCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<MonasteryCardGameShellViewModel, MonasteryCardInfo>(customDeck: true);
            OurContainer!.RegisterType<BasicGameLoader<MonasteryCardGamePlayerItem, MonasteryCardGameSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<MonasteryCardGamePlayerItem, MonasteryCardGameSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<MonasteryCardGamePlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>();
            return Task.CompletedTask;
        }
    }
}