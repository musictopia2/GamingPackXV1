using System;
using System.Text;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using fs = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.FileHelpers;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
using Rummy500CP.ViewModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using Rummy500CP.Data;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
//i think this is the most common things i like to do
namespace Rummy500Blazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<Rummy500ShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterCommonRegularCards<Rummy500ShellViewModel, RegularRummyCard>(aceLow: false);
            OurContainer!.RegisterType<BasicGameLoader<Rummy500PlayerItem, Rummy500SaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<Rummy500PlayerItem, Rummy500SaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<Rummy500PlayerItem>>(true); //had to be set to true after all.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularAceHighCalculator>();
            return Task.CompletedTask;
        }
    }
}