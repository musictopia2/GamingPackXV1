using BasicGamingUIBlazorLibrary.BasicControls.GameBoards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using System.Linq;
using TicTacToeCP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace TicTacToeBlazor
{
    public class LineComponent : ComponentBase
    {

        [CascadingParameter]
        public GridGameBoard<SpaceInfoCP>? Parent { get; set; }

        [Parameter]
        public WinInfo? Win { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Win == null || Parent == null || Win.IsDraw == true || Win.WinList.Count != 3)
            {
                return;
            }
            SvgRenderClass render = new SvgRenderClass();
            ISvg svg = new SVG();
            var firstSpace = Win.WinList.First();
            var lastSpace = Win.WinList.Last();
            PointF firstPoint = Parent.GetControlLocation(firstSpace.Vector.Row, firstSpace.Vector.Column);
            PointF lastPoint = Parent.GetControlLocation(lastSpace.Vector.Row, lastSpace.Vector.Column); //pretend its going from one to another.
            var targetspace = Parent.TargetSpaceHeight;
            var divides = Parent.TargetSpaceWidth / 2;
            switch (Win.Category)
            {
                case EnumWinCategory.TopDown:
                    firstPoint.X += divides;
                    firstPoint.Y += 20;
                    lastPoint.X += divides;
                    lastPoint.Y += targetspace - 20;
                    break;
                case EnumWinCategory.LeftRight:
                    firstPoint.X += 20;
                    firstPoint.Y += divides;
                    lastPoint.X += targetspace - 20;
                    lastPoint.Y += divides;
                    break;
                case EnumWinCategory.TopLeft:
                    //doing top left first.
                    firstPoint.X += 20;
                    firstPoint.Y += 20;
                    lastPoint.X += targetspace;
                    lastPoint.Y += targetspace;
                    lastPoint.X -= 20;
                    lastPoint.Y -= 20;
                    break;
                case EnumWinCategory.TopRight:
                    //next is top right.
                    firstPoint.X += targetspace;
                    firstPoint.X -= 20;
                    firstPoint.Y += 20;
                    lastPoint.X += 20;
                    lastPoint.Y += targetspace;
                    lastPoint.Y -= 20;
                    break;
                default:
                    break;
            }
            svg.DrawLine(firstPoint, lastPoint, cc.Red.ToWebColor(), 20, .5);
            render.RenderSvgTree(svg, 0, builder);
        }
    }
}