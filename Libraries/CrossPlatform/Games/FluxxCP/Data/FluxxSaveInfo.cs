using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using FluxxCP.Cards;
using FluxxCP.Containers;
namespace FluxxCP.Data
{
    [SingletonGame]
    public class FluxxSaveInfo : BasicSavedCardClass<FluxxPlayerItem, FluxxCardInformation>
    {
        public DeckRegularDict<GoalCard> GoalList { get; set; } = new DeckRegularDict<GoalCard>();
        public DeckRegularDict<RuleCard> RuleList { get; set; } = new DeckRegularDict<RuleCard>();
        public CustomBasicList<int> DelayedPlayList { get; set; } = new CustomBasicList<int>();
        public bool DoAnalyze { get; set; }
        public CustomBasicList<int> QueList { get; set; } = new CustomBasicList<int>();
        public SavedActionClass SavedActionData { get; set; } = new SavedActionClass();
        public int CurrentAction { get; set; }
        private int _playsLeft;
        public int PlaysLeft
        {
            get { return _playsLeft; }
            set
            {
                if (SetProperty(ref _playsLeft, value))
                {
                    if (_model != null)
                    {
                        _model.PlaysLeft = value;
                    }
                }
            }
        }
        private int _handLimit;
        public int HandLimit
        {
            get { return _handLimit; }
            set
            {
                if (SetProperty(ref _handLimit, value))
                {
                    if (_model != null)
                    {
                        _model.HandLimit = value;
                    }
                }
            }
        }
        private int _keeperLimit;
        public int KeeperLimit
        {
            get { return _keeperLimit; }
            set
            {
                if (SetProperty(ref _keeperLimit, value))
                {
                    if (_model != null)
                    {
                        _model.KeeperLimit = value;
                    }
                }
            }
        }
        private int _playLimit;
        public int PlayLimit
        {
            get { return _playLimit; }
            set
            {
                if (SetProperty(ref _playLimit, value))
                {
                    if (_model != null)
                    {
                        _model.PlayLimit = value;
                    }
                }
            }
        }
        private bool _anotherTurn;
        public bool AnotherTurn
        {
            get { return _anotherTurn; }
            set
            {
                if (SetProperty(ref _anotherTurn, value))
                {
                    if (_model != null)
                    {
                        _model.AnotherTurn = value;
                    }
                }
            }
        }
        private int _drawBonus;
        public int DrawBonus
        {
            get { return _drawBonus; }
            set
            {
                if (SetProperty(ref _drawBonus, value))
                {
                    if (_model != null)
                    {
                        _model.DrawBonus = value;
                    }
                }
            }
        }
        private int _playBonus;
        public int PlayBonus
        {
            get { return _playBonus; }
            set
            {
                if (SetProperty(ref _playBonus, value))
                {
                    if (_model != null)
                    {
                        _model.PlayBonus = value;
                    }
                }
            }
        }
        private int _cardsDrawn;
        public int CardsDrawn
        {
            get { return _cardsDrawn; }
            set
            {
                if (SetProperty(ref _cardsDrawn, value))
                {
                    if (_model != null)
                    {
                        _model.CardsDrawn = value;
                    }
                }
            }
        }
        private int _cardsPlayed;
        public int CardsPlayed
        {
            get { return _cardsPlayed; }
            set
            {
                if (SetProperty(ref _cardsPlayed, value))
                {
                    if (_model != null)
                    {
                        _model.CardsPlayed = value;
                    }
                }
            }
        }
        private int _drawRules;
        public int DrawRules
        {
            get { return _drawRules; }
            set
            {
                if (SetProperty(ref _drawRules, value))
                {
                    if (_model != null)
                    {
                        _model.DrawRules = value;
                    }
                }
            }
        }
        private int _previousBonus;
        public int PreviousBonus
        {
            get { return _previousBonus; }
            set
            {
                if (SetProperty(ref _previousBonus, value))
                {
                    if (_model != null)
                    {
                        _model.PreviousBonus = value;
                    }
                }
            }
        }
        private FluxxVMData? _model;
        internal void LoadMod(FluxxVMData model)
        {
            model.PlaysLeft = PlaysLeft;
            model.HandLimit = HandLimit;
            model.KeeperLimit = KeeperLimit;
            model.PlayLimit = PlayLimit;
            model.AnotherTurn = AnotherTurn;
            model.DrawBonus = DrawBonus;
            model.PlayBonus = PlayBonus;
            model.CardsDrawn = CardsDrawn;
            model.CardsPlayed = CardsPlayed;
            model.DrawRules = DrawRules;
            model.PreviousBonus = PreviousBonus;
            _model = model;
        }
    }
}