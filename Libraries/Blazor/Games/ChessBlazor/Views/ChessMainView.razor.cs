using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
using BasicGameFrameworkLibrary.TestUtilities;
using ChessCP.Data;
using ChessCP.Logic;
using ChessCP.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace ChessBlazor.Views
{
    public partial class ChessMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? GameBoard { get; set; }
        private ChessGameContainer? GameContainer { get; set; }
        protected override void OnInitialized()
        {
            GameBoard = cons!.Resolve<GameBoardGraphicsCP>(); //hopefully this simple this time.
            GameContainer = cons.Resolve<ChessGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ChessMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(ChessMainViewModel.Instructions))
                 .AddLabel("Status", nameof(ChessMainViewModel.Status));
            base.OnInitialized();
        }
        private int LongestSize => CheckersChessBaseBoard<EnumColorChoice, SpaceCP>.LongestSize;
        private string EndMethod => nameof(ChessMainViewModel.EndTurnAsync);
        private string UndoMethod => nameof(ChessMainViewModel.UndoMovesAsync);
        private string TieMethod => nameof(ChessMainViewModel.TieAsync);
        private int HighlightedIndex(SpaceCP space)
        {
            int tempIndex;
            if (GameContainer!.BasicData.MultiPlayer == false)
            {
                tempIndex = space.ReversedIndex;
            }
            else
            {
                tempIndex = space.MainIndex;
            }
            return tempIndex;
        }
        private string GetPreviousColor => GameContainer!.SaveRoot.PreviousMove.PlayerColor.ToWebColor();
        private EnumPieceType GetCategory(CheckerChessPieceCP<EnumColorChoice> piece)
        {
            PieceCP output = (PieceCP)piece;
            return output.WhichPiece;
        }
    }
}