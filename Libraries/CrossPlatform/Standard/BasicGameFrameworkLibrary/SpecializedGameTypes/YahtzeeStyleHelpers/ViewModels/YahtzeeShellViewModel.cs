using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels
{
    public class YahtzeeShellViewModel<D> : BasicMultiplayerShellViewModel<YahtzeePlayerItem<D>>
        where D : SimpleDice, new()
    {
        public YahtzeeShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test) : base(mainContainer, container, gameData, basicData, save, test)
        {
        }
        protected override CustomBasicList<Type> GetAdditionalObjectsToReset()
        {
            CustomBasicList<Type> output = new CustomBasicList<Type>()
            {
                typeof(IYahtzeeStyle),
                typeof(ScoreLogic),
                typeof(ScoreContainer),
                typeof(YahtzeeGameContainer<D>),
                typeof(YahtzeeEndRoundLogic<D>),
                typeof(YahtzeeMove<D>),
                typeof(YahtzeeVMData<D>)
            };
            return output;
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<YahtzeeMainViewModel<D>>();
            return model;
        }
    }
}