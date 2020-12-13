using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Cards;
namespace LifeBoardGameCP.Data
{
    [SingletonGame]
    public class LifeBoardGameSaveInfo : BasicSavedGameClass<LifeBoardGamePlayerItem>
    { 
        private EnumWhatStatus _gameStatus;
        public EnumWhatStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    if (_model != null)
                    {
                        _model.GameStatus = value;
                    }
                }
            }
        }
        public CustomBasicList<TileInfo> TileList { get; set; } = new CustomBasicList<TileInfo>();
        public DeckRegularDict<HouseInfo> HouseList { get; set; } = new DeckRegularDict<HouseInfo>();
        public bool WasMarried { get; set; }
        public bool GameStarted { get; set; }
        public bool WasNight { get; set; }
        public int MaxChosen { get; set; }
        public int NewPosition { get; set; }
        public bool EndAfterSalary { get; set; }
        public bool EndAfterStock { get; set; }
        public int NumberRolled { get; set; }
        public int SpinPosition { get; set; }
        public int ChangePosition { get; set; }
        public CustomBasicList<int> SpinList { get; set; } = new CustomBasicList<int>(); //needs this so knows for entertainer getting 100,000 dollars.
        public int TemporarySpaceSelected { get; set; }
        private LifeBoardGameVMData? _model;
        internal void LoadMod(LifeBoardGameVMData model)
        {
            _model = model;
            _model.GameStatus = GameStatus;
        }
    }
}