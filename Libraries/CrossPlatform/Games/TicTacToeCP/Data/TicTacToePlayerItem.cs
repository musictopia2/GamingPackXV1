using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace TicTacToeCP.Data
{
    public class TicTacToePlayerItem : SimplePlayer
    {
        private EnumSpaceType _piece;
        public EnumSpaceType Piece
        {
            get { return _piece; }
            set
            {
                if (SetProperty(ref _piece, value))
                {
                    
                }
            }
        }
    }
}