using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.GameGraphics.GamePieces;
using CheckersCP.Data;
using CheckersCP.Logic;
using CheckersCP.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using System.Linq;

namespace CheckersBlazor.Views
{
    public partial class CheckersMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? GameBoard { get; set; }
        private CheckersGameContainer? GameContainer { get; set; }

        private bool CanRenderSpace => GameContainer!.PlayerList!.First().Color != EnumColorChoice.None;
        protected override void OnInitialized()
        {
            GameBoard = cons!.Resolve<GameBoardGraphicsCP>();
            GameContainer = cons.Resolve<CheckersGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(CheckersMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(CheckersMainViewModel.Instructions))
                 .AddLabel("Status", nameof(CheckersMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(CheckersMainViewModel.EndTurnAsync);
        private string TieMethod => nameof(CheckersMainViewModel.TieAsync);
        private string PieceColor(CheckerChessPieceCP<EnumColorChoice> piece)
        {
            if (piece.Highlighted)
            {
                return cs.Yellow;
            }
            return piece.EnumValue.ToColor();
        }
        private string PieceColor(EnumColorChoice color) => color.ToColor();
        private EnumCheckerPieceCategory CheckerCategory(CheckerChessPieceCP<EnumColorChoice> piece)
        {
            CheckerPieceCP output = (CheckerPieceCP)piece;
            if (output.IsCrowned)
            {
                return EnumCheckerPieceCategory.CrownedPiece;
            }
            return EnumCheckerPieceCategory.SinglePiece;
        }
        private int LongestSize => CheckersChessBaseBoard<EnumColorChoice, SpaceCP>.LongestSize;
        private EnumCheckerPieceCategory AnimationCategory
        {
            get
            {
                if (GameContainer!.CurrentCrowned)
                {
                    return EnumCheckerPieceCategory.CrownedPiece;
                }
                return EnumCheckerPieceCategory.SinglePiece;
            }
        }
    }
}