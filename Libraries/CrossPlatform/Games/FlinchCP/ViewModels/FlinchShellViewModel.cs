using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using FlinchCP.Data;
using FlinchCP.Logic;
using System;
namespace FlinchCP.ViewModels
{
    public class FlinchShellViewModel : BasicMultiplayerShellViewModel<FlinchPlayerItem>
    {
        public FlinchShellViewModel(IGamePackageResolver mainContainer,
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
            var model = MainContainer.Resolve<FlinchMainViewModel>();
            return model;
        }
        protected override CustomBasicList<Type> GetAdditionalObjectsToReset()
        {
            return new CustomBasicList<Type>(){ typeof(FlinchComputerAI)};
        }
    }
}