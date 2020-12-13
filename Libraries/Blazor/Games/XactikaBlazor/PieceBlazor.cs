using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using System.Drawing;
using System.Reflection;
using XactikaCP.Data;
namespace XactikaBlazor
{
    public class PieceBlazor : ComponentBase
    {
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }
        [Parameter]
        public int HowMany { get; set; }
        [Parameter]
        public EnumShapes ShapeUsed { get; set; }
        protected override void OnInitialized()
        {
            MainGraphics!.OriginalSize = new SizeF(60, 138);
            MainGraphics.NeedsHighlighting = true;
            base.OnInitialized();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (ShapeUsed == EnumShapes.None)
            {
                return;
            }
            ISvg svg = MainGraphics!.GetMainSvg(false);
            SvgRenderClass render = new SvgRenderClass();
            Assembly assembly = Assembly.GetAssembly(GetType());
            var thisHeight = 40;
            var thisSize = new SizeF(thisHeight, thisHeight);
            var pointList = ImageHelpers.GetPoints(ShapeUsed, HowMany, MainGraphics.Location, true, thisHeight); // can always be adjusted.   test on desktop first anyways.
            foreach (var thisPoint in pointList)
            {
                var thisRect = new RectangleF(thisPoint, thisSize);
                switch (ShapeUsed)
                {
                    case EnumShapes.None:
                        break;
                    case EnumShapes.Balls:
                        ImageHelpers.DrawBall(svg, thisRect);
                        break;
                    case EnumShapes.Cubes:
                        ImageHelpers.DrawCube(svg, assembly, thisRect);
                        break;
                    case EnumShapes.Cones:
                        ImageHelpers.DrawCone(svg, assembly, thisRect);
                        break;
                    case EnumShapes.Stars:
                        ImageHelpers.DrawStar(svg, thisRect);
                        break;
                    default:
                        break;
                }
            }
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}