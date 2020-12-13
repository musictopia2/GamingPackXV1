using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
namespace ChineseCheckersCP.Data
{
    [SingletonGame]
    public class ChineseCheckersSaveInfo : BasicSavedGameClass<ChineseCheckersPlayerItem>
    {
        public CustomBasicList<MoveInfo> PreviousMoves { get; set; } = new CustomBasicList<MoveInfo>();
        public bool TurnContinued { get; set; }
        public int PreviousSpace { get; set; }
        private string _instructions = "None";
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    if (_gameContainer == null || _gameContainer.Model == null)
                    {
                        return;
                    }
                    if (value != "None")
                    {
                        _gameContainer.Model.Instructions = value;
                    }
                }
            }
        }
        private ChineseCheckersGameContainer? _gameContainer;
        public void Init(ChineseCheckersGameContainer gameContainer)
        {
            _gameContainer = gameContainer;
            if (_gameContainer.Model == null)
            {
                throw new BasicBlankException("Model not populated.  Rethink");
            }
            if (Instructions != "None" && Instructions != "")
            {
                _gameContainer.Model!.Instructions = Instructions;
            }
        }
    }
}