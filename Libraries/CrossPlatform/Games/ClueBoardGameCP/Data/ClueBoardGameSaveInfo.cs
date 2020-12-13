using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using System.Collections.Generic;
namespace ClueBoardGameCP.Data
{
    [SingletonGame]
    public class ClueBoardGameSaveInfo : BasicSavedBoardDiceGameClass<ClueBoardGamePlayerItem>
    {
        private string _instructions = "";
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    if (_model != null)
                    {
                        _model.Instructions = value;
                    }
                }
            }
        }
        private ClueBoardGameVMData? _model;
        internal void LoadMod(ClueBoardGameVMData model)
        {
            _model = model;
            _model.Instructions = Instructions;
            _model.LeftToMove = MovesLeft;
        }
        public int DiceNumber { get; set; }
        public PredictionInfo? CurrentPrediction { get; set; }
        private int _movesLeft;
        public int MovesLeft
        {
            get { return _movesLeft; }
            set
            {
                if (SetProperty(ref _movesLeft, value))
                {
                    if (_model != null)
                    {
                        _model.LeftToMove = MovesLeft;
                    }
                }
            }
        }
        public bool AccusationMade { get; set; }
        public bool ShowedMessage { get; set; }
        public Dictionary<int, CharacterInfo> CharacterList { get; set; } = new Dictionary<int, CharacterInfo>();
        public PredictionInfo Solution { get; set; } = new PredictionInfo();
        public Dictionary<int, MoveInfo> PreviousMoves { get; set; } = new Dictionary<int, MoveInfo>();
        public Dictionary<int, WeaponInfo> WeaponList { get; set; } = new Dictionary<int, WeaponInfo>(); //needs this also.
        public EnumClueStatusList GameStatus { get; set; }
    }
}