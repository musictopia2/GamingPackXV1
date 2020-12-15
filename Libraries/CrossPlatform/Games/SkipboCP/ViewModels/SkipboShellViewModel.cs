using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using SkipboCP.Data;
using SkipboCP.Logic;
using System;

namespace SkipboCP.ViewModels
{
    public class SkipboShellViewModel : BasicMultiplayerShellViewModel<SkipboPlayerItem>
    {
        public SkipboShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test)
            : base(mainContainer, container, gameData, basicData, save, test)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<SkipboMainViewModel>();
            return model;
        }
        protected override CustomBasicList<Type> GetAdditionalObjectsToReset()
        {
            return new CustomBasicList<Type>() { typeof(SkipboComputerAI) };
        }
    }
}