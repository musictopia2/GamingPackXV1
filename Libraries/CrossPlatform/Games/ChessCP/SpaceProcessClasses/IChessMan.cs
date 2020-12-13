using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
using ChessCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
namespace ChessCP.SpaceProcessClasses
{
    public interface IChessMan
    {
        int Row { get; set; }
        int Column { get; set; }
        int Player { get; set; }
        bool IsReversed { get; set; }
        EnumPieceType PieceCategory { get; set; }
        CustomBasicList<CheckersChessVector> GetValidMoves();
    }
}