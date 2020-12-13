using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
namespace CheckersCP.Data
{
    public class SpaceCP : CheckersChessSpace<EnumColorChoice>
    {
        public int PlayerOwns { get; set; }
        public bool IsCrowned { get; set; }
        public EnumColorChoice PlayerColor { get; set; }
        public override void ClearSpace()
        {
            PlayerOwns = 0;
            IsCrowned = false;
            PlayerColor = EnumColorChoice.None;
        }
        public override CheckerChessPieceCP<EnumColorChoice>? GetGamePiece()
        {
            if (PlayerOwns == 0)
            {
                return null;
            }
            CheckerPieceCP output = new CheckerPieceCP();
            output.EnumValue = PlayerColor;
            output.IsCrowned = IsCrowned;
            return output;
        }
        protected override EnumCheckerChessGame GetGame()
        {
            return EnumCheckerChessGame.Checkers;
        }
    }
}