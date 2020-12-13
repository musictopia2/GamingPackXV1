using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using ClueBoardGameCP.Cards;
using ClueBoardGameCP.Data;
using ClueBoardGameCP.Logic;
using CommonBasicStandardLibraries.Exceptions;
using System.Threading.Tasks;
namespace ClueBoardGameCP.ViewModels
{
    [InstanceGame]
    public class ClueBoardGameMainViewModel : BoardDiceGameVM
    {
        private readonly ClueBoardGameMainGameClass _mainGame;
        private readonly ClueBoardGameVMData _model;
        private readonly ClueBoardGameGameContainer _gameContainer;
        private readonly GameBoardProcesses _gameBoard;
        public ClueBoardGameMainViewModel(CommandContainer commandContainer,
            ClueBoardGameMainGameClass mainGame,
            ClueBoardGameVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IStandardRollProcesses roller,
            ClueBoardGameGameContainer gameContainer,
            GameBoardProcesses gameBoard
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver, roller)
        {
            _mainGame = mainGame;
            _model = model;
            _gameContainer = gameContainer;
            _gameBoard = gameBoard;
            _gameContainer.SpaceClickedAsync = MoveSpaceAsync;
            _gameContainer.RoomClickedAsync = MoveRoomAsync;
            _model.Pile.SendEnableProcesses(this, () => false);
            _model.HandList.ObjectClickedAsync += HandList_ObjectClickedAsync;
            _model.HandList.SendEnableProcesses(this, () => _mainGame.SaveRoot.GameStatus == EnumClueStatusList.FindClues);
        }
        public void PopulateDetectiveNoteBook()
        {
            _mainGame.PopulateDetectiveNoteBook();
        }
        public HandObservable<CardInfo> GetHand => _model.HandList;
        public SingleObservablePile<CardInfo> GetPile => _model.Pile;

        private int _leftToMove;
        [VM]
        public int LeftToMove
        {
            get { return _leftToMove; }
            set
            {
                if (SetProperty(ref _leftToMove, value))
                {
                    
                }
            }
        }

        private string _currentRoomName = "";
        [VM]
        public string CurrentRoomName
        {
            get { return _currentRoomName; }
            set
            {
                if (SetProperty(ref _currentRoomName, value))
                {
                    
                }
            }
        }
        private string _currentCharacterName = "";
        [VM]
        public string CurrentCharacterName
        {
            get { return _currentCharacterName; }
            set
            {
                if (SetProperty(ref _currentCharacterName, value))
                {
                    
                }
            }
        }
        private string _currentWeaponName = "";
        [VM]
        public string CurrentWeaponName
        {
            get { return _currentWeaponName; }
            set
            {
                if (SetProperty(ref _currentWeaponName, value))
                {
                    
                }
            }
        }
        public DiceCup<SimpleDice> GetCup => _model.Cup!;
        private async Task MoveSpaceAsync(int space)
        {
            if (_gameContainer.Test.DoubleCheck)
            {
                _gameContainer.TempClicked = space;
                _gameBoard.RepaintBoard();
                return;
            }
            if (_gameBoard.CanMoveToSpace(space) == false)
            {
                return;
            }
            if (_mainGame.SaveRoot.MovesLeft == 0)
            {
                return;
            }
            if (_mainGame.BasicData.MultiPlayer)
            {
                await _mainGame.Network!.SendAllAsync("space", space);
            }
            _gameBoard.MoveToSpace(space);
            await _mainGame.ContinueMoveAsync();
        }

        private async Task MoveRoomAsync(int room)
        {
            if (_gameBoard.CanMoveToRoom(room) == false)
            {
                return;
            }
            if (_mainGame!.SaveRoot!.GameStatus == EnumClueStatusList.MoveSpaces && _gameContainer.CurrentCharacter!.PreviousRoom > 0)
            {
                return;
            }
            if (_gameContainer.BasicData!.MultiPlayer)
            {
                await _gameContainer.Network!.SendAllAsync("room", room);
            }
            _gameContainer.SaveRoot.MovesLeft = 0;
            _gameBoard.MoveToRoom(room);
            _gameContainer.SaveRoot.GameStatus = EnumClueStatusList.MakePrediction; //hopefully this is it.
        }

        private async Task HandList_ObjectClickedAsync(CardInfo payLoad, int index)
        {
            if (_gameContainer.CanGiveCard(payLoad) == false)
            {
                return;
            }
            payLoad.IsSelected = true;
            CommandContainer.UpdateAll(); //to notify blazor.
            if (_gameContainer.Test.NoAnimations == false)
            {
                await _gameContainer.Delay.DelaySeconds(.25);
            }
            var tempPlayer = _gameContainer!.PlayerList!.GetWhoPlayer();
            if (_gameContainer.BasicData!.MultiPlayer)
                await _gameContainer.Network!.SendToParticularPlayerAsync("cluegiven", payLoad.Deck, tempPlayer.NickName);
            CommandContainer!.ManuelFinish = true;
            payLoad.IsSelected = false;
            _gameContainer.SaveRoot!.GameStatus = EnumClueStatusList.EndTurn;
            if (_gameContainer.BasicData.MultiPlayer == false)
                throw new BasicBlankException("Computer should have never had this");
            _gameContainer.Check!.IsEnabled = true; //to wait for them to end turn.
        }
        [Command(EnumCommandCategory.Game)]
        public void CurrentRoomClick(DetectiveInfo room)
        {
            _model.CurrentRoomName = room.Name;
        }
        [Command(EnumCommandCategory.Game)]
        public void CurrentCharacterClick(DetectiveInfo character)
        {
            _model.CurrentCharacterName = character.Name;
        }

        [Command(EnumCommandCategory.Game)]
        public void CurrentWeaponClick(DetectiveInfo weapon) //had to try to change to detectiveinfo to support blazor.
        {
            _model.CurrentWeaponName = weapon.Name;
        }
        public bool CanMakePrediction
        {
            get
            {
                if (CurrentWeaponName == "" || CurrentCharacterName == "")
                {
                    return false;
                }
                if (_gameContainer.SaveRoot.GameStatus == EnumClueStatusList.MakePrediction)
                {
                    return true;
                }
                if (_gameContainer.SaveRoot.GameStatus == EnumClueStatusList.StartTurn)
                {
                    if (_gameContainer.CurrentCharacter!.PreviousRoom != _gameContainer.CurrentCharacter.CurrentRoom)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        [Command(EnumCommandCategory.Game)]
        public async Task MakePredictionAsync()
        {
            _gameContainer!.SaveRoot!.CurrentPrediction!.CharacterName = CurrentCharacterName;
            _gameContainer.SaveRoot.CurrentPrediction.WeaponName = CurrentWeaponName;
            await _mainGame.MakePredictionAsync();
        }
        public bool CanMakeAccusation
        {
            get
            {
                if (_gameContainer.SaveRoot.GameStatus == EnumClueStatusList.FindClues)
                {
                    return false;
                }
                if (CurrentWeaponName == "" || CurrentCharacterName == "" || CurrentRoomName == "")
                {
                    return false;
                }
                return true;
            }
        }
        [Command(EnumCommandCategory.Game)]
        public async Task MakeAccusationAsync()
        {
            _gameContainer!.SaveRoot!.CurrentPrediction!.CharacterName = CurrentCharacterName;
            _gameContainer.SaveRoot.CurrentPrediction.WeaponName = CurrentWeaponName;
            _gameContainer.SaveRoot.CurrentPrediction.RoomName = CurrentRoomName;
            await _mainGame.MakeAccusationAsync();
        }
        protected override Task TryCloseAsync()
        {
            _model.HandList.ObjectClickedAsync -= HandList_ObjectClickedAsync;
            return base.TryCloseAsync();
        }
        public override bool CanRollDice()
        {
            return _gameContainer.SaveRoot.GameStatus == EnumClueStatusList.StartTurn;
        }
        public override bool CanEndTurn()
        {
            return _gameContainer.SaveRoot.GameStatus == EnumClueStatusList.MakePrediction || _gameContainer.SaveRoot.GameStatus == EnumClueStatusList.EndTurn;
        }
        public override async Task RollDiceAsync()
        {
            await base.RollDiceAsync();
        }
        [Command(EnumCommandCategory.Limited)]
        public void FillInClue(DetectiveInfo detective)
        {
            detective.IsChecked = !detective.IsChecked;
        }
    }
}