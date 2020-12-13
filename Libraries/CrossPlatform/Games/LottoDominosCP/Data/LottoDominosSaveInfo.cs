using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace LottoDominosCP.Data
{
    [SingletonGame]
    public class LottoDominosSaveInfo : BasicSavedGameClass<LottoDominosPlayerItem>
    {
        public DeckRegularDict<SimpleDominoInfo> ComputerList = new DeckRegularDict<SimpleDominoInfo>();
        public DeckRegularDict<SimpleDominoInfo>? BoardDice { get; set; }
        private EnumStatus _gameStatus;
        public EnumStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {

                }
            }
        }
    }
}