using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
using ChessCP.Data;
namespace ChessCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP : CheckersChessBaseBoard<EnumColorChoice, SpaceCP>
    {
        private readonly ChessGameContainer _gameContainer;
        public GameBoardGraphicsCP(ChessGameContainer gameContainer)
        {
            _gameContainer = gameContainer;
        }
        protected override void AfterClearBoard()
        {
            _gameContainer.SpaceList = PrivateSpaceList;
        }
        public override bool CanHighlight()
        {
            if (_gameContainer.SaveRoot!.SpaceHighlighted > 0)
            {
                return true;
            }
            return _gameContainer.SaveRoot.PreviousMove.SpaceFrom > 0 && _gameContainer.SaveRoot.PreviousMove.SpaceTo > 0;
        }
        public override EnumCheckerChessGame GetGame()
        {
            return EnumCheckerChessGame.Chess;
        }
    }
}