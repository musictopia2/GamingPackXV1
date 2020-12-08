using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainGameInterfaces;
using BasicGameFrameworkLibrary.TestUtilities;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels
{
    public abstract class TrickCardGamesVM<T, S> : BasicCardGamesVM<T>
        where S : struct, Enum
        where T : class, ITrickCard<S>, new()
    {
        public TrickCardGamesVM(CommandContainer commandContainer,
            ICardGameMainProcesses<T> mainGame,
            IBasicCardGamesData<T> viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver) : base(commandContainer, mainGame, viewModel, basicData, test, resolver) { }

        private S _trumpSuit;
        [VM]
        public S TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value)) { }
            }
        }

    }
}