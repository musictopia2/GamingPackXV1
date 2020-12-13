using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
using CheckersCP.Data;
namespace CheckersCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP : CheckersChessBaseBoard<EnumColorChoice, SpaceCP>
    {
        private readonly CheckersGameContainer _container;

        public GameBoardGraphicsCP(CheckersGameContainer container)
        {
            _container = container;
        }
        protected override void AfterClearBoard()
        {
            _container.SpaceList = PrivateSpaceList;
        }

        public override bool CanHighlight()
        {
            return _container.SaveRoot!.SpaceHighlighted > 0;
        }
        public override EnumCheckerChessGame GetGame()
        {
            return EnumCheckerChessGame.Checkers;
        }       
    }
}