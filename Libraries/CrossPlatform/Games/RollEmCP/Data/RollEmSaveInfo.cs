using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace RollEmCP.Data
{
    [SingletonGame]
    public class RollEmSaveInfo : BasicSavedDiceClass<SimpleDice, RollEmPlayerItem>
    {
        public CustomBasicList<string> SpaceList { get; set; } = new CustomBasicList<string>();
        public EnumStatusList GameStatus { get; set; }
        private int _round;
        public int Round
        {
            get { return _round; }
            set
            {
                if (SetProperty(ref _round, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.Round = value;
                }
            }
        }
        private RollEmVMData? _model;
        internal void LoadMod(RollEmVMData model)
        {
            _model = model;
            _model.Round = Round;
        }
    }
}