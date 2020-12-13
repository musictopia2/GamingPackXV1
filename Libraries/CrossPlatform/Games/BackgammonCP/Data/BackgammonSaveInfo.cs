using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace BackgammonCP.Data
{
    [SingletonGame]
    public class BackgammonSaveInfo : BasicSavedBoardDiceGameClass<BackgammonPlayerItem>
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
        private int _movesMade;
        public int MovesMade
        {
            get { return _movesMade; }
            set
            {
                if (SetProperty(ref _movesMade, value))
                {
                    if (_model != null)
                    {
                        _model.MovesMade = value;
                    }
                }
            }
        }
        private BackgammonVMData? _model;
        internal void LoadMod(BackgammonVMData model)
        {
            _model = model;
            _model.Instructions = Instructions;
            _model.MovesMade = MovesMade;
        }
        public int SpaceHighlighted { get; set; } = -1;
        public int NumberUsed { get; set; }
        //since its human only now, maybe no need for this part now.

        //public int ComputerSpaceTo { get; set; }
        public bool MadeAtLeastOneMove { get; set; }
        public EnumGameStatus GameStatus { get; set; }
    }
}