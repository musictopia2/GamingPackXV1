using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
namespace ChessCP.Data
{
    public class SpaceCP : CheckersChessSpace<EnumColorChoice>
    {
        public int PlayerOwns { get; set; }
        public EnumColorChoice PlayerColor { get; set; }
        public EnumPieceType PlayerPiece { get; set; }
        public override void ClearSpace()
        {
            PlayerOwns = 0;
            PlayerPiece = EnumPieceType.None;
        }
        protected override EnumCheckerChessGame GetGame()
        {
            return EnumCheckerChessGame.Chess;
        }
        public override CheckerChessPieceCP<EnumColorChoice>? GetGamePiece()
        {
            if (PlayerOwns == 0)
            {
                return null;
            }
            PieceCP output = new PieceCP();
            output.EnumValue = PlayerColor;
            output.WhichPiece = PlayerPiece;
            return output;
        }
    }
}