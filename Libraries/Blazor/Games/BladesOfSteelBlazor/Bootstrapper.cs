using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BladesOfSteelCP.Data;
using BladesOfSteelCP.ViewModels;
using System.Threading.Tasks;
namespace BladesOfSteelBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BladesOfSteelShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<BladesOfSteelShellViewModel, RegularSimpleCard>(aceLow: false);
            OurContainer!.RegisterType<BasicGameLoader<BladesOfSteelPlayerItem, BladesOfSteelSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<BladesOfSteelPlayerItem, BladesOfSteelSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<BladesOfSteelPlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            return Task.CompletedTask;
        }
    }
}