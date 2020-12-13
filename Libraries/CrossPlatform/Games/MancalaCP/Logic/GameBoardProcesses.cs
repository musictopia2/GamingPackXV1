using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using MancalaCP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
namespace MancalaCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardProcesses
    {
        private bool _didReverse;
        private PlayerPieceData? _currentPiece;
        private MancalaPlayerItem? _currentPlayer;
        private readonly IEventAggregator _aggregator;
        private readonly MancalaVMData _vmdata;
        private readonly BasicData _basicData;
        private readonly TestOptions _test;
        private readonly IAsyncDelayer _delayer;
        public GameBoardProcesses(IEventAggregator aggregator,
            MancalaVMData vmdata,
            BasicData basicData,
            TestOptions test,
            IAsyncDelayer delayer
            )
        {
            _aggregator = aggregator;
            _vmdata = vmdata;
            _basicData = basicData;
            _test = test;
            _delayer = delayer;
        }
        #region "delegates"
        //this is to stop the overflows.  this is what the main game process has to populate.  if i sent in the game, then overflow since the game needs this one too.  not worth doing container this time.  can rethink later.
        public Func<PlayerCollection<MancalaPlayerItem>>? PlayerList { get; set; }
        public Func<MancalaPlayerItem>? SingleInfo { get; set; }
        public Action<MancalaPlayerItem>? SetCurrentPlayer { get; set; }
        public Func<Task>? EndTurnAsync { get; set; }
        public Func<int>? WhoTurn { get; set; }
        public Func<Task>? ContinueTurnAsync { get; set; }
        public Func<Task>? ShowTieAsync { get; set; }
        public Func<Task>? ShowWinAsync { get; set; }
        public Func<MancalaSaveInfo>? SaveRoot { get; set; }
        #endregion
        public CustomBasicList<SpaceInfo> GetSpaces => _vmdata.SpaceList.Values.ToCustomBasicList();
        internal void CreateSpaceList()
        {
            _vmdata.SpaceList = new Dictionary<int, SpaceInfo>();
            14.Times(x =>
            {
                SpaceInfo space = new SpaceInfo();
                space.Pieces = 0;
                switch (x)
                {
                    case 1:
                        space.Bounds = new RectangleF(40, 40, 33, 33);
                        break;
                    case 2:
                        space.Bounds = new RectangleF(40, 79, 33, 33);
                        break;
                    case 3:
                        space.Bounds = new RectangleF(40, 118, 33, 33);
                        break;
                    case 4:
                        space.Bounds = new RectangleF(40, 157, 33, 33);
                        break;
                    case 5:
                        space.Bounds = new RectangleF(40, 196, 33, 33);
                        break;
                    case 6:
                        space.Bounds = new RectangleF(40, 235, 33, 33);
                        break;
                    case 7:
                        space.Bounds = new RectangleF(1, 274, 73, 33);
                        break;
                    case 8:
                        space.Bounds = new RectangleF(1, 235, 33, 33);
                        break;
                    case 9:
                        space.Bounds = new RectangleF(1, 196, 33, 33);
                        break;
                    case 10:
                        space.Bounds = new RectangleF(1, 157, 33, 33);
                        break;
                    case 11:
                        space.Bounds = new RectangleF(1, 118, 33, 33);
                        break;
                    case 12:
                        space.Bounds = new RectangleF(1, 79, 33, 33);
                        break;
                    case 13:
                        space.Bounds = new RectangleF(1, 40, 33, 33);
                        break;
                    case 14:
                        space.Bounds = new RectangleF(1, 1, 73, 33);
                        break;
                    default:
                        break;
                }
                _vmdata.SpaceList.Add(x, space);
            });
        }
        public void RepaintBoard()
        {
            _aggregator.RepaintBoard();
        }
        public void LoadSavedBoard()
        {
            PopulateBoard();
            RepaintBoard();
        }
        private void PopulateBoard()
        {
            int x = 0;
            bool wasReversed;
            int index;
            int counts2 = _vmdata.SpaceList!.Values.Sum(items => items.Pieces);
            if (counts2 != 48 && counts2 > 0)
            {
                throw new BasicBlankException($"Count Of {counts2} does not reconcile with 48 Part 4");
            }
            int counts1 = PlayerList!.Invoke().First().ObjectList.Sum(Items => Items.HowManyPieces) + PlayerList.Invoke().First().HowManyPiecesAtHome;
            counts2 = PlayerList!.Invoke().Last().ObjectList.Sum(Items => Items.HowManyPieces) + PlayerList.Invoke().Last().HowManyPiecesAtHome;
            int Totals = counts1 + counts2;
            if (Totals != 48)
            {
                throw new BasicBlankException($"Count Of {Totals} does not reconcile with 48 part 7");
            }
            foreach (var Space in _vmdata.SpaceList.Values)
            {
                Space.Pieces = 0;
            }
            PlayerList!.Invoke().ForEach(thisPlayer =>
            {
                x++;
                wasReversed = NeedsReversed(x);
                if (wasReversed == false)
                {
                    index = 7;
                }
                else
                {
                    index = 14;
                }
                if (index > 0)
                {
                    var tempSpace = _vmdata.SpaceList[index];
                    tempSpace.Pieces = thisPlayer.HowManyPiecesAtHome;
                }
                thisPlayer.ObjectList.ForEach(ThisObject =>
                {
                    if (wasReversed == false)
                        index = ThisObject.Index;
                    else
                        index = ThisObject.Index + 7;
                    var TempSpace = _vmdata.SpaceList[index];
                    TempSpace.Pieces = ThisObject.HowManyPieces;
                });
            });
            int counts = _vmdata.SpaceList.Values.Sum(Items => Items.Pieces);
            if (counts != 48)
            {
                throw new BasicBlankException($"Count Of {counts} does not reconcile with 48 Part 5");
            }
        }
        public void ClearBoard()
        {
            PlayerList!.Invoke().ForEach(thisPlayer =>
            {
                thisPlayer.HowManyPiecesAtHome = 0;
                thisPlayer.ObjectList = new CustomBasicList<PlayerPieceData>();
                int x;
                for (x = 1; x <= 6; x++)
                {
                    PlayerPieceData thisData = new PlayerPieceData();
                    thisData.Index = x;
                    thisData.HowManyPieces = 4;
                    thisPlayer.ObjectList.Add(thisData);
                }
            });
            PopulateBoard();
            RepaintBoard();
        }
        internal void Reset()
        {
            _vmdata.SpaceSelected = 0;
            _vmdata.SpaceStarted = 0;
        }
        public async Task StartNewTurnAsync()
        {
            SaveRoot!.Invoke().IsStart = false;
            PopulateBoard();
            _vmdata.PiecesAtStart = 0;
            _vmdata.PiecesLeft = 0;
            Reset();
            await ContinueTurnAsync!.Invoke();
        }
        private bool NeedsReversed(int playerConsidered)
        {
            if (_basicData.MultiPlayer == true)
            {
                var tempPlayer = PlayerList!.Invoke()[playerConsidered];
                if (tempPlayer.PlayerCategory == EnumPlayerCategory.OtherHuman)
                {
                    return true;
                }
                return false;
            }
            if (WhoTurn!.Invoke() == playerConsidered)
            {
                return false;
            }
            return true;
        }
        public async Task AnimateMoveAsync(int index)
        {
            _didReverse = NeedsReversed(WhoTurn!.Invoke());
            _vmdata.SpaceStarted = index;
            int nums;
            int whatNum;
            whatNum = index;
            var thisSpace = _vmdata.SpaceList![index]; // i think
            nums = thisSpace.Pieces;
            if (nums == 0)
            {
                throw new BasicBlankException("Can't have 0 when animating move.");
            }
            _vmdata.PiecesAtStart = nums;
            thisSpace.Pieces = 0;
            if (_currentPiece == null == true)
            {
                if (_didReverse == false)
                    _currentPiece = (from x in SingleInfo!.Invoke().ObjectList
                                     where x.Index == index
                                     select x).Single();
                else
                    _currentPiece = (from x in SingleInfo!.Invoke().ObjectList
                                     where x.Index == (index - 7)
                                     select x).Single();
                _currentPlayer = SingleInfo.Invoke();
            }
            _currentPlayer!.ObjectList.RemoveSpecificItem(_currentPiece!);
            _currentPiece = null; // something else has to come along to fill that piece now.
            _currentPlayer = null;
            do
            {
                whatNum += 1;
                if (whatNum > 14)
                {
                    whatNum = 1;
                }
                if (CanProcess(whatNum) == true)
                {
                    ProcessMove(whatNum);
                    nums -= 1;
                    _vmdata.PiecesLeft = nums;
                    if (_test.NoAnimations == false)
                    {
                        await _delayer.DelayMilli(400);
                    }
                    if (nums == 0)
                    {
                        await EndProcessesAsync(whatNum);
                        return;
                    }
                }
            }
            while (true);
        }
        private void ProcessMove(int index)
        {
            _vmdata.SpaceSelected = index;
            SpaceInfo thisSpace;
            thisSpace = _vmdata.SpaceList![index];
            bool isOpponent;
            if (index != 14 && index != 7)
            {
                int yourSpace;
                if (_didReverse == false)
                {
                    yourSpace = index;
                    if (yourSpace > 6)
                    {
                        yourSpace = index - 7;
                        isOpponent = true; // forgot this part.
                    }
                    else
                    {
                        isOpponent = false;
                    }
                }
                else
                {
                    yourSpace = index - 7;
                    if (yourSpace < 0)
                    {
                        yourSpace = index;
                        isOpponent = true;
                    }
                    else
                    {
                        isOpponent = false;
                    }
                }
                if (yourSpace < 1 || yourSpace > 6)
                {
                    throw new BasicBlankException("The space must be 1 to 6, not " + yourSpace);
                }
                if (isOpponent == false)
                {
                    _currentPlayer = SingleInfo!.Invoke();
                }
                else
                {
                    if (WhoTurn!.Invoke() == 1)
                    {
                        _currentPlayer = PlayerList!.Invoke()[2];
                    }
                    else
                    {
                        _currentPlayer = PlayerList!.Invoke()[1];
                    }
                }
                _currentPiece = (from items in _currentPlayer!.ObjectList
                                 where items.Index == yourSpace
                                 select items).SingleOrDefault();
                if (_currentPiece == null == true)
                {
                    _currentPiece = new PlayerPieceData();
                    _currentPiece.Index = yourSpace; // i forgot this part.
                    _currentPlayer.ObjectList.Add(_currentPiece);
                }
            }
            thisSpace.Pieces += 1; //decided to transfer after its done.  because sometimes it got hosed.
            RepaintBoard();
        }
        internal void TransferBeadsToPlayers()
        {
            if (_vmdata.SpaceList!.Values.Sum(Items => Items.Pieces) != 48)
            {
                throw new BasicBlankException("Does not reconcile before transferring beads to players");
            }
            PlayerList!.Invoke().ForEach(player =>
            {
                bool isReversed = NeedsReversed(player.Id);
                player.ObjectList.Clear();
                if (isReversed == false)
                {
                    player.HowManyPiecesAtHome = _vmdata.SpaceList[7].Pieces;
                }
                else
                {
                    player.HowManyPiecesAtHome = _vmdata.SpaceList[14].Pieces;
                }
                for (int i = 1; i <= 6; i++)
                {
                    int index;
                    if (isReversed == false)
                    {
                        index = i;
                    }
                    else
                    {
                        index = i + 7;
                    }
                    if (_vmdata.SpaceList[index].Pieces > 0)
                    {
                        PlayerPieceData thisPiece = new PlayerPieceData();
                        thisPiece.HowManyPieces = _vmdata.SpaceList[index].Pieces;
                        thisPiece.Index = i; //i think.
                        player.ObjectList.Add(thisPiece);
                    }
                }
            });
        }
        private async Task EndProcessesAsync(int index)
        {
            if (index == 7 || index == 14)
            {
                await ContinueProcessesAsync(false);
                return;
            }
            SpaceInfo thisSpace;
            thisSpace = _vmdata.SpaceList![index];
            if (thisSpace.Pieces > 1)
            {
                await AnimateMoveAsync(index);
                return;
            }
            if (IsOnOwnSide(index) == false)
            {
                await ContinueProcessesAsync(true);
                return;
            }
            int nums;
            nums = 14 - index;
            nums = Math.Abs(nums);
            thisSpace = _vmdata.SpaceList[nums];
            if (thisSpace.Pieces == 0)
            {
                await ContinueProcessesAsync(true);
                return;
            }
            await AnimateMoveAsync(nums);
        }
        private async Task ContinueProcessesAsync(bool endTurn)
        {
            TransferBeadsToPlayers();
            if (AnyOnSide(true) == false)
            {
                await GameOverProcessesAsync();
                return;
            }
            if (AnyOnSide(false) == false)
            {
                await GameOverProcessesAsync();
                return;
            }
            if (endTurn == true)
            {
                _currentPiece = null;
                await EndTurnAsync!.Invoke();
                return;
            }
            _currentPiece = null; // because you have to make another move
            await ContinueTurnAsync!.Invoke();
        }
        private async Task GameOverProcessesAsync()
        {
            _vmdata.Instructions = "";
            if (PlayerList!.Invoke().First().HowManyPiecesAtHome == PlayerList!.Invoke().Last().HowManyPiecesAtHome)
            {
                await ShowTieAsync!.Invoke();
                return;
            }
            SetCurrentPlayer!(PlayerList.Invoke().OrderByDescending(Items => Items.HowManyPiecesAtHome).First());
            await ShowWinAsync!.Invoke();
        }
        private bool CanProcess(int whatNum)
        {
            if (_didReverse == false)
            {
                if (whatNum == 14)
                {
                    return false;
                }
                return true;
            }
            if (whatNum == 7)
            {
                return false;
            }
            return true;
        }
        private bool IsOnOwnSide(int index)
        {
            if (_didReverse == false && index <= 6)
            {
                return true;
            }
            if (_didReverse == true && index > 7)
            {
                return true;
            }
            return false;
        }
        private bool AnyOnSide(bool firstSide)
        {
            int y;
            if (firstSide == true)
            {
                y = 1;
            }
            else
            {
                y = 8;
            }
            int x;
            SpaceInfo thisSpace;
            var loopTo = y + 5;
            for (x = y; x <= loopTo; x++)
            {
                thisSpace = _vmdata.SpaceList![x];
                if (thisSpace.Pieces > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}