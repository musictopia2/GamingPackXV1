using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CheckersCP.Logic;
namespace CheckersCP.Data
{
    [SingletonGame]
    public class CheckersSaveInfo : BasicSavedGameClass<CheckersPlayerItem>
    {
        public int SpaceHighlighted
        {
            get
            {
                return GameBoardGraphicsCP.SpaceSelected;
            }
            set
            {
                GameBoardGraphicsCP.SpaceSelected = value;
            }
        }
        public EnumGameStatus GameStatus { get; set; }
        public bool ForcedToMove { get; set; }
    }
}