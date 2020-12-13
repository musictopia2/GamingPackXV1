using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ConcentrationCP.Data;
using ConcentrationCP.Logic;
using ConcentrationCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ConcentrationBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ConcentrationShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<ConcentrationShellViewModel, RegularSimpleCard>(customDeck: true);
            OurContainer!.RegisterType<BasicGameLoader<ConcentrationPlayerItem, ConcentrationSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<ConcentrationPlayerItem, ConcentrationSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<ConcentrationPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>();
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}