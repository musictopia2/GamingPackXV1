using BackgammonCP.Data;
using BackgammonCP.ViewModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BackgammonBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<BackgammonShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<BackgammonShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<BackgammonPlayerItem, BackgammonSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, BackgammonPlayerItem, BackgammonSaveInfo>(true);
            return Task.CompletedTask;
        }
    }
}