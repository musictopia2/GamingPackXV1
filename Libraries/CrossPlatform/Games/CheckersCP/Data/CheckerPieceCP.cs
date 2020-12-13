using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
namespace CheckersCP.Data
{
    public class CheckerPieceCP : CheckerChessPieceCP<EnumColorChoice>
    {
        public bool IsCrowned { get; set; }
    }
}