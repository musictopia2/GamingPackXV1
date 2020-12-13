using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using RookCP.Cards;
namespace RookCP.Data
{
    [SingletonGame]
    public class RookSaveInfo : BasicSavedTrickGamesClass<EnumColorTypes, RookCardInformation, RookPlayerItem>
    {
        public int WonSoFar { get; set; }
        public bool DummyPlay { get; set; }
        public int HighestBidder { get; set; }
        public DeckRegularDict<RookCardInformation> NestList { get; set; } = new DeckRegularDict<RookCardInformation>();
        public DeckRegularDict<RookCardInformation> DummyList { get; set; } = new DeckRegularDict<RookCardInformation>();
        public DeckRegularDict<RookCardInformation> CardList { get; set; } = new DeckRegularDict<RookCardInformation>();
        private EnumStatusList _gameStatus;
        public EnumStatusList GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.GameStatus = value;
                }
            }
        }
        private RookVMData? _model;
        public void LoadMod(RookVMData model)
        {
            _model = model;
            _model.GameStatus = GameStatus;
        }
    }
}