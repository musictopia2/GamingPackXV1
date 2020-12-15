using BackgammonCP.Data;
using BackgammonCP.Graphics;
using BasicGamingUIBlazorLibrary.GameGraphics.GamePieces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
namespace BackgammonBlazor
{
    public partial class BackgammonCompleteBoard
    {
        [CascadingParameter]
        public GameBoardGraphicsCP? GraphicsData { get; set; } //decided to cascade it because something else needs it as well.
        [Parameter]
        public string TargetHeight { get; set; } = "";
        private string GetColor() => GetColor(GraphicsData!.GameContainer.SingleInfo!);
        private string GetColor(BackgammonPlayerItem player)
        {
            return player.Color.ToColor();
        }
        private EnumCheckerPieceCategory GetCategory(int index)
        {
            if (index == 25 || index == 26)
            {
                return EnumCheckerPieceCategory.FlatPiece;
            }
            return EnumCheckerPieceCategory.OnlyPiece;
        }
        protected override bool ShouldRender()
        {
            return GraphicsData!.GameContainer.SingleInfo!.Color != EnumColorChoice.None;
        }
    }
}