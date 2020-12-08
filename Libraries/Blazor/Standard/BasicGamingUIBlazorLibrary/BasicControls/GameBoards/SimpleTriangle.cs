using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public class SimpleTriangle : ComponentBase
    {
        public enum EnumTriangleInfo
        {
            BottomLeft,
            BottomCenter,
            BottomRight,
            TopLeft,
            TopCenter,
            TopRight
        }

        [Parameter]
        public EnumTriangleInfo TriangleInfo { get; set; }
        [Parameter]
        public RectangleF Bounds { get; set; }
        [Parameter]
        public string BorderColor { get; set; } = cc.Transparent;
        [Parameter]
        public string FillColor { get; set; } = cc.Transparent;
        [Parameter]
        public int BorderWidth { get; set; } = 3;

        private float GetCenterX => (Bounds.X + Bounds.Right) / 2;

        private CustomBasicList<PointF> GetTrianglePoints()
        {
            PointF topLeft;
            PointF topCenter;
            PointF topRight;

            PointF bottomLeft;
            PointF bottomCenter;
            PointF bottomRight;

            topLeft = new PointF(Bounds.X, Bounds.Y);
            topCenter = new PointF(GetCenterX, Bounds.Y);

            topRight = new PointF(Bounds.Right, Bounds.Y);

            bottomLeft = new PointF(Bounds.X, Bounds.Bottom);
            bottomCenter = new PointF(GetCenterX, Bounds.Bottom);
            bottomRight = new PointF(Bounds.Right, Bounds.Bottom);

            return TriangleInfo switch
            {
                EnumTriangleInfo.BottomLeft => new CustomBasicList<PointF>() { bottomLeft, topLeft, bottomRight },
                EnumTriangleInfo.BottomCenter => new CustomBasicList<PointF>() { bottomLeft, topCenter, bottomRight },
                EnumTriangleInfo.BottomRight => new CustomBasicList<PointF>() { bottomLeft, topRight, bottomRight },
                EnumTriangleInfo.TopLeft => new CustomBasicList<PointF>() { topLeft, bottomLeft, topRight },
                EnumTriangleInfo.TopCenter => new CustomBasicList<PointF>() { topLeft, bottomCenter, topRight },
                EnumTriangleInfo.TopRight => new CustomBasicList<PointF>() { topLeft, bottomRight, topRight },
                _ => new CustomBasicList<PointF>() { },
            };
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (BorderColor == cc.Transparent && FillColor == cc.Transparent)
            {
                return;
            }
            SvgRenderClass render = new SvgRenderClass();
            ISvg svg = new SVG();
            CustomBasicList<PointF> points = GetTrianglePoints();
            Polygon poly = points.CreatePolygon();
            poly.Fill = FillColor.ToWebColor(); //i think.
            if (BorderColor != cc.Transparent)
            {
                poly.PopulateStrokesToStyles(BorderColor.ToWebColor(), BorderWidth);
            }
            svg.Children.Add(poly);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
        protected override bool ShouldRender()
        {
            return false; //hopefully not needed anymore since this should be fixed
        }
    }
}