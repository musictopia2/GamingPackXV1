using ChessCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace ChessBlazor
{
    public partial class ChessPieceBlazor
    {
        [Parameter]
        public EnumColorChoice Color { get; set; }

        [Parameter]
        public int LongestSize { get; set; }

        [Parameter]
        public PointF Location { get; set; }

        [Parameter]
        public EnumPieceType PieceCategory { get; set; }
        private string GetMainStyle()
        {
            return $"fill:{Color.ToColor().ToWebColor()}; stroke:#000000; stroke-linecap:butt;";
        }
        private string GetFill()
        {
            return $"fill: { Color.ToColor().ToWebColor()};";
        }
    }
}