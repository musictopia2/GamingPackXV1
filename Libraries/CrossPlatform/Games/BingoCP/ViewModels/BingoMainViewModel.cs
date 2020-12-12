using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BingoCP.Data;
using BingoCP.Logic;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using System.Timers;
namespace BingoCP.ViewModels
{
    [InstanceGame]
    public class BingoMainViewModel : BasicMultiplayerMainVM
    {
        private readonly BingoMainGameClass _mainGame; //if we don't need, delete.
        private readonly BasicData _basicData;
        private readonly TestOptions _test;
        private readonly Timer _timer;
        public BingoMainViewModel(CommandContainer commandContainer,
            BingoMainGameClass mainGame,
            IViewModelData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _basicData = basicData;
            _test = test;
            _timer = new Timer();
            _timer.Enabled = false;
            _timer.Interval = 6000;
            _timer.Elapsed += TimerElapsed;
            _mainGame.SetTimerEnabled = ((rets =>
            {
                _timer.Enabled = rets;
            }));
        }
        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            CommandContainer!.ManuelFinish = true;
            CommandContainer.IsExecuting = true;
            _timer.Enabled = false;
            if (_basicData.MultiPlayer == true)
            {
                if (_basicData.Client == true)
                {
                    _mainGame!.Check!.IsEnabled = true; //maybe needs to be after you checked.  or it could get hosed.
                    return; //has to wait for host.
                }
                if (_mainGame!.PlayerList.Any(Items => Items.PlayerCategory == EnumPlayerCategory.Computer))
                {
                    await _mainGame.FinishAsync();
                    return;
                }
                await _mainGame.Network!.SendAllAsync("callnextnumber");
                await _mainGame.CallNextNumberAsync();
                return;
            }
            await _mainGame!.FinishAsync();
        }
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            if (_basicData.MultiPlayer && _basicData.Client)
            {
                await _mainGame.CallNextNumberAsync();
            }
        }
        private string _numberCalled = "";
        [VM]
        public string NumberCalled
        {
            get { return _numberCalled; }
            set
            {
                if (SetProperty(ref _numberCalled, value))
                {
                    
                }

            }
        }
        [Command(EnumCommandCategory.Game)]
        public async Task BingoAsync()
        {
            BingoPlayerItem selfPlayer = _mainGame!.PlayerList!.GetSelf();
            if (selfPlayer.BingoList.HasBingo == false)
            {
                string oldStatus = Status;
                Status = "No Bingos Here";
                await _mainGame!.Delay!.DelayMilli(500);
                Status = oldStatus;
                return;
            }
            if (_basicData!.MultiPlayer == true)
            {
                await _mainGame.Network!.SendAllAsync("bingo", selfPlayer.Id);
            }
            await _mainGame.GameOverAsync(selfPlayer.Id);
        }
        public bool CanSelectSpace(SpaceInfoCP space)
        {
            if (space.IsEnabled == false)
                return false;
            if (space.AlreadyMarked == true)
                return false;
            if (space.Text == "Free")
                return true;
            if (space.Text == _mainGame.CurrentInfo!.WhatValue.ToString())
                return true;
            if (_test.AllowAnyMove == true)
                return true;
            return false;
        }
        [Command(EnumCommandCategory.Game)]
        public void SelectSpace(SpaceInfoCP space)
        {
            space.AlreadyMarked = true;
            BingoPlayerItem selfPlayer = _mainGame.PlayerList!.GetSelf();
            var thisBingo = selfPlayer.BingoList[space.Vector.Row - 1, space.Vector.Column];
            thisBingo.DidGet = true;
        }
    }
}