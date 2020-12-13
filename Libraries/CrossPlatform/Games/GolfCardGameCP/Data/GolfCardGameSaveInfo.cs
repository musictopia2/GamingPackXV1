using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace GolfCardGameCP.Data
{
    [SingletonGame]
    public class GolfCardGameSaveInfo : BasicSavedCardClass<GolfCardGamePlayerItem, RegularSimpleCard>
    {
        public EnumStatusType GameStatus { get; set; } //still needed here so it can communicate with the client when its next round, etc.
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
        private GolfCardGameVMData? _model;
        internal void LoadMod(GolfCardGameVMData model)
        {
            _model = model;
            model.Round = Round;
        }
    }
}