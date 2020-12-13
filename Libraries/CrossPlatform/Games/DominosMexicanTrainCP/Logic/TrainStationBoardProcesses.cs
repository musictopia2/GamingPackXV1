using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using DominosMexicanTrainCP.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace DominosMexicanTrainCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class TrainStationBoardProcesses
    {
        public struct PrivateTrain
        {
            public bool IsRotated;
            public bool IsOpposite;
            public bool IsBottom;
            public RectangleF DominoArea;
            public RectangleF TrainArea;
        }
        public Dictionary<int, PrivateTrain> PrivateList = new Dictionary<int, PrivateTrain>(); //looks like the main has to now handle this.
        //if i wanted broken into another class, not sure what names to use.
        private int NewestTrain
        {
            get
            {
                return _saveRoot!.NewestTrain;
            }
            set
            {
                _saveRoot!.NewestTrain = value;
            }
        }
        public int UpTo
        {
            get
            {
                return _saveRoot!.UpTo;
            }
            set
            {
                _saveRoot!.UpTo = value;
            }
        }
        private readonly IEventAggregator _thisE;
        private readonly GlobalClass _thisGlobal;
        private readonly TestOptions _test;
        private DominosMexicanTrainSaveInfo? _saveRoot;
        public TrainStationBoardProcesses(IEventAggregator thisE,
            GlobalClass thisGlobal,
            TestOptions test
            )
        {
            _thisE = thisE;
            _thisGlobal = thisGlobal;
            _test = test;
            thisGlobal.TrainStation1 = this;
        }

        public AnimateBasicGameBoard GetAnimation => _thisGlobal.Animates!;
        public MexicanDomino GetMovingDomino => _thisGlobal.MovingDomino!;
        private void FirstLoad()
        {
            PrivateList.Clear();
            PrivateTrain thisPrivate;
            int x;
            for (x = 1; x <= 8; x++)
            {
                thisPrivate = new PrivateTrain();
                if (x == 1)
                {
                    //275
                    thisPrivate.DominoArea = new RectangleF(225, 0, 150, 225); //test this one.
                    thisPrivate.TrainArea = new RectangleF(263, 225, 75, 46);
                    thisPrivate.IsRotated = true; // domino
                    thisPrivate.IsOpposite = true;
                }
                else if (x == 2)
                {
                    thisPrivate.DominoArea = new RectangleF(375, 0, 150, 225);
                    thisPrivate.TrainArea = new RectangleF(412, 225, 75, 46);
                    thisPrivate.IsRotated = true; // domino
                    thisPrivate.IsOpposite = true;
                }
                else if (x == 3)
                {
                    thisPrivate.DominoArea = new RectangleF(525, 225, 225, 150);
                    // this piece will need to be rotated for the train piece (well see)
                    thisPrivate.TrainArea = new RectangleF(450, 277, 75, 46);
                    thisPrivate.IsRotated = false;
                    thisPrivate.IsOpposite = false;
                }
                else if (x == 4)
                {
                    thisPrivate.DominoArea = new RectangleF(525, 375, 225, 150);
                    thisPrivate.TrainArea = new RectangleF(450, 427, 75, 46);
                    thisPrivate.IsBottom = true;
                    thisPrivate.IsRotated = false;
                    thisPrivate.IsOpposite = false;
                }
                else if (x == 5)
                {
                    thisPrivate.DominoArea = new RectangleF(375, 525, 150, 225);
                    thisPrivate.TrainArea = new RectangleF(412, 479, 75, 46);
                    thisPrivate.IsRotated = true;
                    thisPrivate.IsOpposite = false;
                }
                else if (x == 6)
                {
                    thisPrivate.DominoArea = new RectangleF(225, 525, 150, 225);
                    thisPrivate.TrainArea = new RectangleF(263, 479, 75, 46);
                    thisPrivate.IsRotated = true;
                    thisPrivate.IsOpposite = false;
                }
                else if (x == 7)
                {
                    thisPrivate.DominoArea = new RectangleF(0, 375, 225, 150);
                    thisPrivate.TrainArea = new RectangleF(225, 427, 75, 46);
                    thisPrivate.IsBottom = true;
                    thisPrivate.IsRotated = false;
                    thisPrivate.IsOpposite = true;
                }
                else if (x == 8)
                {
                    thisPrivate.DominoArea = new RectangleF(0, 225, 225, 150);
                    thisPrivate.TrainArea = new RectangleF(225, 277, 75, 46);
                    thisPrivate.IsRotated = false;
                    thisPrivate.IsOpposite = true;
                }
                PrivateList.Add(x, thisPrivate);
            }
        }
        public bool CanEndEarly()
        {
            if (_thisGlobal.BoneYard!.HasBone())
            {
                return false;
            }
            return !_thisGlobal.BoneYard.HasDrawn();
        }

        public void SavedData()
        {
            if (_saveRoot == null)
            {
                throw new BasicBlankException("Save Root was not set when getting saved data.  Rethink");
            }
            SavedTrain output = new SavedTrain();
            output.Satisfy = Satisfy;
            output.CenterDomino = CenterDomino;
            output.TrainList = TrainList;
            _saveRoot.TrainData = output;
        }
        public async Task LoadSavedDataAsync(DominosMexicanTrainSaveInfo saveRoot)
        {
            _saveRoot = saveRoot;
            SavedTrain output = _saveRoot.TrainData!;
            Satisfy = output.Satisfy;
            CenterDomino = output.CenterDomino!;
            TrainList = output.TrainList;
            int x = 0;
            foreach (var thisTrain in TrainList.Values)
            {
                x++;
                var tempList = thisTrain.DominoList.ToRegularDeckDict();
                thisTrain.DominoList.Clear();
                tempList.ForEach(thisDomino =>
                {
                    thisTrain.DominoList.Add(thisDomino);
                });
            }
            RepaintBoard();
            await Task.CompletedTask;
        }
        public void LoadPlayers(int highestDomino, DominosMexicanTrainMainGameClass mainGame)
        {
            FirstLoad();
            _saveRoot = mainGame.SaveRoot;
            LoadBoard(mainGame.PlayerList.Count());
            if (Self == 0)
            {
                throw new BasicBlankException("Needs to know self");
            }
            if (UpTo == -1 && _test.DoubleCheck == false)
            {
                UpTo = highestDomino;
            }
            else if (_test.DoubleCheck == true)
            {
                UpTo = 0;
            }
            NewestTrain = 0;
        }
        private void LoadBoard(int players)
        {
            if (PrivateList.Count == 0)
            {
                throw new BasicBlankException("Sorry; there are no items on the private list.  Run FirstLoad first before loading the board");
            }
            if (TrainList.Count > 0)
            {
                throw new BasicBlankException("There are already items on the train list");
            }
            if (Self > 7)
            {
                throw new BasicBlankException("The self player has to be between 1 and 7");
            }
            CustomBasicList<int> newList;
            if (players == 2)
            {
                newList = new CustomBasicList<int>() { 1, 3, 6 };
            }
            else if (players == 3)
            {
                newList = new CustomBasicList<int>() { 1, 3, 6, 8 };
            }
            else if (players == 4)
            {
                newList = new CustomBasicList<int>() { 1, 2, 3, 6, 8 };
            }
            else if (players == 5)
            {
                newList = new CustomBasicList<int>();
                for (var x = 1; x <= 5; x++)
                    newList.Add(x);
                newList.Add(8);
            }
            else if (players == 6)
            {
                newList = new CustomBasicList<int>();
                for (var x = 1; x <= 6; x++)
                    newList.Add(x);
                newList.Add(8);
            }
            else if (players == 7)
            {
                newList = GetIntegerList(1, 8);
            }
            else
            {
                throw new BasicBlankException("Sorry; the new list does not match.  Find out what happened");
            }
            for (var x = 1; x <= players + 1; x++)
            {
                TrainInfo thisTrain = new TrainInfo();
                thisTrain.Index = newList[x - 1];
                if (x == players + 1)
                {
                    thisTrain.TrainUp = true;
                    thisTrain.IsPublic = true;
                }
                TrainList.Add(thisTrain);
            }
            RepaintBoard();
        }
        public void StartRound()
        {
            CenterDomino = _thisGlobal.BoneYard!.FindDoubleDomino(UpTo); //i think
            RepaintBoard();
        }
        public async Task EndRoundAsync(DominosMexicanTrainMainGameClass mainGame)
        {
            UpTo--;
            if (UpTo < 0)
            {
                await mainGame.GameOverAsync();
                return;
            }
            await mainGame.RoundOverNextAsync();
        }
        private void ClearBoard()
        {
            if (TrainList.Count == 0)
            {
                throw new BasicBlankException("There is no trains shown");
            }
            if (TrainList.Count < 3)
            {
                throw new BasicBlankException("There has to be at least 3 trains because 2 players plus public");
            }
            if (TrainList.Count > 8)
            {
                throw new BasicBlankException("The most trains can be 8 because 7 players max plus public");
            }
            Satisfy = 0;
            int x = 0;
            foreach (var thisTrain in TrainList.Values)
            {
                x++;
                thisTrain.DominoList = new DeckRegularDict<MexicanDomino>();
                thisTrain.TrainUp = x == TrainList.Count;
            }
        }
        public void NewRound()
        {
            NewestTrain = 0;
            ClearBoard();
            StartRound();
        }
        public TrainInfo GetTrain(int index)
        {
            return TrainList[index];
        }
        public void StartTurn()
        {
            RepaintBoard();
        }
        public void RepaintBoard()
        {
            _thisE.RepaintBoard();
        }
        public Dictionary<int, TrainInfo> TrainList = new Dictionary<int, TrainInfo>();
        public int Self { get; set; }
        public MexicanDomino CenterDomino { get; set; } = new MexicanDomino();
        public int Satisfy { get; set; }
        public CustomBasicList<int> FindAvailablePlays(int turn)
        {
            CustomBasicList<int> output = new CustomBasicList<int>();
            if (Satisfy > 0)
            {
                output.Add(Satisfy);
                return output;
            }
            int x = 0;
            foreach (var thisTrain in TrainList.Values)
            {
                x++;
                if (x == turn || thisTrain.TrainUp)
                {
                    output.Add(x);
                }
            }
            return output;
        }
        public int MiddleDominoDeck => CenterDomino.Deck;
        public bool CanFillPrevious(PlayerCollection<DominosMexicanTrainPlayerItem> players, int turn)
        {
            if (CanEndEarly() == false)
            {
                return false;
            }
            if (Satisfy > 0)
            {
                return true;
            }
            var thisCol = FindAvailablePlays(turn);
            bool rets = thisCol.Count < players.Count() + 1;
            return !rets;
        }
        public void RemoveTrain(int player, PlayerCollection<DominosMexicanTrainPlayerItem> players)
        {
            if (player == players.Count() + 1)
            {
                return;
            }
            var thisTrain = GetTrain(player);
            thisTrain.TrainUp = false;
            if (NewestTrain == player)
            {
                NewestTrain = 0;
            }
            RepaintBoard();
        }
        public bool NeedDouble(out int numberNeeded)
        {
            numberNeeded = -1;
            if (Satisfy == 0)
            {
                return false;
            }
            numberNeeded = DominoNeeded(Satisfy);
            return true;
        }
        private MexicanDomino GetLastDomino(TrainInfo thisTrain)
        {
            if (thisTrain.DominoList.Count == 0)
            {
                return CenterDomino;
            }
            return thisTrain.DominoList.Last();
        }
        public int DominoNeeded(int index)
        {
            var thisTrain = TrainList[index];
            var thisDomino = GetLastDomino(thisTrain);
            if (thisDomino.FirstNum == thisDomino.SecondNum)
            {
                return thisDomino.FirstNum;
            }
            var thisPrivate = PrivateList[thisTrain.Index];
            if (thisPrivate.IsOpposite)
            {
                return thisDomino.CurrentFirst;
            }
            return thisDomino.CurrentSecond;
        }
        public void PutTrain(int player, PlayerCollection<DominosMexicanTrainPlayerItem> players)
        {
            if (player == players.Count() + 1)
            {
                return;
            }
            var thisTrain = GetTrain(player);
            thisTrain.TrainUp = true;
            NewestTrain = player;
            RepaintBoard();
        }
        public bool CanPlacePiece(MexicanDomino thisDomino, int player)
        {
            if (_test.AllowAnyMove)
            {
                return true;
            }
            int newNumber = DominoNeeded(player);
            return thisDomino.FirstNum == newNumber || thisDomino.SecondNum == newNumber;
        }
        private void AddDomino(int index, MexicanDomino thisDomino)
        {
            var thisTrain = TrainList[index];
            bool doubles = thisDomino.FirstNum == thisDomino.SecondNum;
            if (thisTrain.DominoList.Count == 2)
            {
                if (thisDomino.FirstNum != thisDomino.SecondNum)
                {
                    thisTrain.DominoList.RemoveAt(1); //because 1 based.
                }
            }
            else if (thisTrain.DominoList.Count == 3)
            {
                thisTrain.DominoList.RemoveAt(2);
                thisTrain.DominoList.RemoveAt(1);
            }
            else if (thisTrain.DominoList.Count > 3)
            {
                throw new BasicBlankException("Cannot have more than 3 items on the list");
            }
            if (thisTrain.DominoList.Count == 0 && doubles)
            {
                throw new BasicBlankException("The first domino played cannot be doubles");
            }
            thisTrain.DominoList.Add(thisDomino);
            if (doubles)
            {
                Satisfy = index;
            }
            else
            {
                Satisfy = 0;
            }
        }
        private void RotateDomino(int index, MexicanDomino newDomino)
        {
            var thisTrain = TrainList[index];
            var thisPrivate = PrivateList[thisTrain.Index];
            if (thisPrivate.IsRotated && newDomino.FirstNum != newDomino.SecondNum)
            {
                newDomino.Rotated = true;
            }
            else if (newDomino.FirstNum != newDomino.SecondNum)
            {
                newDomino.Rotated = false;
            }
            else if (thisPrivate.IsRotated)
            {
                newDomino.Rotated = false;
            }
            else
            {
                newDomino.Rotated = true;
            }
            if (newDomino.FirstNum == newDomino.SecondNum)
            {
                return;
            }
            var oldDomino = GetLastDomino(thisTrain);
            if (thisPrivate.IsOpposite)
            {
                if (newDomino.SecondNum == oldDomino.CurrentFirst)
                {
                    newDomino.CurrentFirst = newDomino.FirstNum;
                    newDomino.CurrentSecond = newDomino.SecondNum;
                    return;
                }
                newDomino.CurrentFirst = newDomino.SecondNum;
                newDomino.CurrentSecond = newDomino.FirstNum;
                return;
            }
            if (newDomino.FirstNum == oldDomino.CurrentSecond)
            {
                newDomino.CurrentFirst = newDomino.FirstNum;
                newDomino.CurrentSecond = newDomino.SecondNum;
                return;
            }
            newDomino.CurrentFirst = newDomino.SecondNum;
            newDomino.CurrentSecond = newDomino.FirstNum;
        }
        public Func<int, int, int, PointF>? DominoLocationNeeded { get; set; }
        public async Task AnimateShowSelectedDominoAsync(int player, MexicanDomino thisDomino, DominosMexicanTrainMainGameClass mainGame)
        {
            if (DominoLocationNeeded == null)
            {
                throw new BasicBlankException("Needs a function for domino needed");
            }
            thisDomino.IsSelected = false;
            thisDomino.Drew = false;
            thisDomino.IsUnknown = false;
            RotateDomino(player, thisDomino);
            _thisGlobal.MovingDomino = thisDomino;
            _thisGlobal.Animates!.LocationFrom = new PointF(5, 5);
            _thisGlobal.Animates.LocationTo = DominoLocationNeeded.Invoke(player, thisDomino.CurrentFirst, thisDomino.CurrentSecond);
            await _thisGlobal.Animates.DoAnimateAsync();
            thisDomino.Location = _thisGlobal.Animates.LocationTo;
            AddDomino(player, thisDomino);
            if (thisDomino.FirstNum == thisDomino.SecondNum)
            {
                mainGame.SaveRoot!.CurrentPlayerDouble = true;
                if (mainGame.SingleInfo!.ObjectCount == 0)
                {
                    await mainGame.EndTurnAsync(true);
                    return;
                }
                _thisGlobal.BoneYard!.NewTurn();
                await mainGame.ContinueTurnAsync();
                return;
            }
            RemoveTrain(mainGame.WhoTurn, mainGame.PlayerList);
            await mainGame.EndTurnAsync(true);
        }
    }
}