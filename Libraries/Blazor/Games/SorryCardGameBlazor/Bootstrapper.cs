using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using SorryCardGameCP.Cards;
using SorryCardGameCP.Data;
using SorryCardGameCP.ViewModels;
using System;
using System.Threading.Tasks;
namespace SorryCardGameBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SorryCardGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SorryCardGameShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<SorryCardGamePlayerItem, SorryCardGameSaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoices, SorryCardGamePlayerItem, SorryCardGameSaveInfo>();
            OurContainer.RegisterType<DeckObservablePile<SorryCardGameCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<SorryCardGameCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, SorryCardGameDeckCount>();
            return Task.CompletedTask;
        }
    }
}