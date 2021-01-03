using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using System.Reflection;
using XactikaCP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace XactikaBlazor
{
    public static class ImageHelpers
    {
        public static CustomBasicList<PointF> GetPoints(EnumShapes shape, int howMany, PointF location, bool manually, float heightWidth)
        {
            float newLeft = 0;
            float margins;
            float mults;
            mults = heightWidth / 16;
            margins = 3 * mults; // for proportions
            if (manually == true)
            {
                if (howMany == 3)
                {
                    return new CustomBasicList<PointF>() { new PointF(location.X + margins, location.Y + margins),
                        new PointF(location.X + margins, location.Y + margins + heightWidth),
                        new PointF(location.X + margins, location.Y + margins + heightWidth * 2) };
                }
                else if (howMany == 1)
                {
                    return new CustomBasicList<PointF>() { new PointF(location.X + margins, location.Y + margins + heightWidth) };
                }

                else
                {
                    return new CustomBasicList<PointF>() { new PointF(location.X + margins, location.Y + margins + (heightWidth / 2)),
                        new PointF(location.X + margins, location.Y + margins + (heightWidth / 2) + heightWidth) };
                }
            }
            float top1;
            float top2;
            float top3;
            float topFirstHalf;
            float topLastHalf;
            top1 = location.Y + margins;
            top2 = top1 + heightWidth + margins;
            top3 = top2 + heightWidth + margins;
            topFirstHalf = location.Y + margins + heightWidth / 2;
            topLastHalf = topFirstHalf + heightWidth + margins;
            switch (shape)
            {
                case EnumShapes.Balls:
                    {
                        newLeft = location.X + margins;
                        break;
                    }
                case EnumShapes.Cubes:
                    {
                        newLeft = location.X + heightWidth + margins * 2;
                        break;
                    }
                case EnumShapes.Cones:
                    {
                        newLeft = location.X + margins + heightWidth * 2 + margins * 2;
                        break;
                    }
                case EnumShapes.Stars:
                    {
                        newLeft = location.X + margins + heightWidth * 3 + margins * 3;
                        newLeft -= 4;
                        //top2 -= 2;
                        break;
                    }
            }
            if (howMany == 3)
            {
                return new CustomBasicList<PointF>() { new PointF(newLeft, top1), new PointF(newLeft, top2), new PointF(newLeft, top3) };
            }
            else if (howMany == 1)
            {
                return new CustomBasicList<PointF>() { new PointF(newLeft, top2) };
            }
            else
            {
                return new CustomBasicList<PointF>() { new PointF(newLeft, topFirstHalf), new PointF(newLeft, topLastHalf) };
            }
        }
        public static void DrawCone(this IParentGraphic container, Assembly assembly, RectangleF bounds) // done
        {
            Image image = new Image();
            image.PopulateFullExternalImage(assembly, "Cone.svg");
            image.PopulateImagePositionings(bounds);
            container.Children.Add(image);
        }
        public static void DrawCube(this IParentGraphic container, Assembly assembly, RectangleF bounds)
        {
            Image image = new Image();
            image.PopulateFullExternalImage(assembly, "Cube.svg");
            image.PopulateImagePositionings(bounds);
            container.Children.Add(image);
        }
        public static void DrawBall(this IParentGraphic container, RectangleF bounds)
        {
            Circle circle = new Circle();
            circle.PopulateCircle(bounds, cc.Red);
            circle.PopulateStrokesToStyles();
            container.Children.Add(circle);
        }
        public static void DrawStar(this IParentGraphic container, RectangleF bounds)
        {

            container.DrawStar(bounds, cc.Yellow, cc.Black, 1);

            //var svg = new SVG();
            //bounds.Width += 2;
            //svg.PopulateSVGStartingPoint(bounds);
            //container.Children.Add(svg);
            //svg.ViewBox = "0 0 40 40";
            //svg.DrawStar(cc.Yellow, cc.Black, 1);
        }
    }
}