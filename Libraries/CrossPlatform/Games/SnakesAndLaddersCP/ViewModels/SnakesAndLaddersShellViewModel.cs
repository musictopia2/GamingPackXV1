using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using SnakesAndLaddersCP.Data;
using System;
namespace SnakesAndLaddersCP.ViewModels
{
    public class SnakesAndLaddersShellViewModel : BasicMultiplayerShellViewModel<SnakesAndLaddersPlayerItem>
    {
        public SnakesAndLaddersShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test)
            : base(mainContainer, container, gameData, basicData, save, test)
        {
        }
        protected override CustomBasicList<Type> GetAdditionalObjectsToReset()
        {
            Type type = typeof(StandardRollProcesses<SimpleDice, SnakesAndLaddersPlayerItem>);
            CustomBasicList<Type> output = new CustomBasicList<Type>()
            {
                type
            };
            return output;
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<SnakesAndLaddersMainViewModel>();
            return model;
        }
    }
}