using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BowlingDiceGameCP.Logic;
using System.Threading.Tasks;
namespace BowlingDiceGameCP.ViewModels
{
    [InstanceGame]
    public class BowlingDiceGameMainViewModel : BasicMultiplayerMainVM
    {
        public readonly BowlingDiceGameMainGameClass MainGame; //if we don't need, delete.

        public BowlingDiceGameMainViewModel(CommandContainer commandContainer,
            BowlingDiceGameMainGameClass mainGame,
            IViewModelData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            MainGame = mainGame;
        }

        private bool _isExtended;
        [VM]
        public bool IsExtended
        {
            get { return _isExtended; }
            set
            {
                if (SetProperty(ref _isExtended, value))
                {
                    
                }
            }
        }
        private int _whichPart;
        [VM]
        public int WhichPart
        {
            get { return _whichPart; }
            set
            {
                if (SetProperty(ref _whichPart, value))
                {
                    
                }
            }
        }
        private int _whatFrame;
        [VM]
        public int WhatFrame
        {
            get { return _whatFrame; }
            set
            {
                if (SetProperty(ref _whatFrame, value))
                {
                    
                }
            }
        }
        private bool _lastTurn;
        public override bool CanEndTurn()
        {
            if (base.CanEndTurn() == false)
            {
                return false;
            }
            if (WhichPart < 3)
            {
                return false;
            }
            if (_lastTurn == true)
            {
                return false;
            }
            return !IsExtended;
        }
        public bool CanContinueTurn => IsExtended;
        [Command(EnumCommandCategory.Game)]
        public async Task ContinueTurnAsync()
        {
            MainGame.SaveRoot.IsExtended = false;
            _lastTurn = true;
            await Task.Delay(10);
            CommandContainer.ManualReport(); //try this.
        }

        public bool CanRoll
        {
            get
            {
                if (WhichPart < 3)
                {
                    return true;
                }
                return _lastTurn;
            }
        }
        [Command(EnumCommandCategory.Game)]
        public async Task RollAsync()
        {
            _lastTurn = false;
            await MainGame.RollDiceAsync();
        }
    }
}