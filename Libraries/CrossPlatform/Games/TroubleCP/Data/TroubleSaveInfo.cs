using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace TroubleCP.Data
{
    [SingletonGame]
    public class TroubleSaveInfo : BasicSavedBoardDiceGameClass<TroublePlayerItem>
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
        private TroubleVMData? _model;
        internal void LoadMod(TroubleVMData model)
        {
            _model = model;
            _model.Instructions = Instructions;
        }
        public CustomBasicList<MoveInfo> MoveList { get; set; } = new CustomBasicList<MoveInfo>();
        public EnumColorChoice OurColor { get; set; }
        public int DiceNumber { get; set; }
    }
}