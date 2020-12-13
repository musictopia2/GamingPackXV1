using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using ChessCP.Logic;
namespace ChessCP.Data
{
    [SingletonGame]
    public class ChessSaveInfo : BasicSavedGameClass<ChessPlayerItem>
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
        public PreviousMove? PossibleMove { get; set; }
        public PreviousMove PreviousMove { get; set; } = new PreviousMove();
    }
}