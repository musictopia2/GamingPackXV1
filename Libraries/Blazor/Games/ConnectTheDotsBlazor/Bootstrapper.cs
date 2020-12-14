using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using ConnectTheDotsCP.Data;
using ConnectTheDotsCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace ConnectTheDotsBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<ConnectTheDotsShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<ConnectTheDotsShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<ConnectTheDotsPlayerItem>>(true); //had to be set to true after all.
            //anything that needs to be registered will be here.


            OurContainer.RegisterType <BeginningColorProcessorClass<EnumColorChoice, ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>>();
            OurContainer.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, ConnectTheDotsPlayerItem>>();
            OurContainer.RegisterType<BeginningColorModel<EnumColorChoice, ConnectTheDotsPlayerItem>>();
            MiscDelegates.GetMiscObjectsToReplace = (() =>
            {
                return MiscDelegates.ReplaceBoardGameColorClasses<EnumColorChoice, ConnectTheDotsPlayerItem, ConnectTheDotsSaveInfo>();
            });

            return Task.CompletedTask;
        }
    }
}