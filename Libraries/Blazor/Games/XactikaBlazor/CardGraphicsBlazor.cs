using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using XactikaCP.Cards;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using SvgHelper.Blazor.Logic;
using XactikaCP.Data;
using System.Reflection;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace XactikaBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<XactikaCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(80, 100);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown || DeckObject!.Value == 0)
            {
                return true;
            }
            return DeckObject.HowManyBalls > 0 || DeckObject.HowManyCones > 0 || DeckObject.HowManyCubes > 0 || DeckObject.HowManyStars > 0;
        }
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Red;
            }
            else
            {
                FillColor = cc.White;
            }
            base.BeforeFilling();
        }
        protected override void DrawBacks()
        {
            var bounds = new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
            var fontSize = bounds.Height * 1.05f;
            DrawText(bounds, "X", fontSize);
        }
        private void DrawText(RectangleF bounds, string value, float fontSize)
        {
            Text text = new Text();
            text.Content = value;
            text.CenterText(MainGroup!, bounds);
            text.Font_Size = fontSize;
            text.Fill = cc.Aqua.ToWebColor();
            text.PopulateStrokesToStyles();
        }
        private void DrawValue(RectangleF bounds)
        {
            var fontSize = bounds.Height * 1.1f;
            DrawText(bounds, DeckObject!.Value.ToString(), fontSize);
        }
        protected override void DrawImage()
        {
            if (DeckObject == null || MainGroup == null)
            {
                return;
            }
            var firstHeight = 40;
            var firstWidth = 16;
            var firstRect = new RectangleF(0, 3, DefaultSize.Width, firstHeight);
            DrawValue(firstRect);
            var otherLocation = new PointF(0, firstRect.Bottom - 7);
            var pointList = ImageHelpers.GetPoints(EnumShapes.Balls, DeckObject.HowManyBalls, otherLocation, false, firstWidth);
            var testSize = new SizeF(firstWidth, firstWidth);
            Assembly assembly = Assembly.GetAssembly(GetType());
            foreach (var thisPoint in pointList)
            {
                var testRect = new RectangleF(thisPoint, testSize);
                ImageHelpers.DrawBall(MainGroup, testRect);
            }
            pointList = ImageHelpers.GetPoints(EnumShapes.Cones, DeckObject.HowManyCones, otherLocation, false, firstWidth);
            foreach (var thisPoint in pointList)
            {
                var testRect = new RectangleF(thisPoint, testSize);
                ImageHelpers.DrawCone(MainGroup, assembly, testRect);
            }
            pointList = ImageHelpers.GetPoints(EnumShapes.Cubes, DeckObject.HowManyCubes, otherLocation, false, firstWidth);
            foreach (var thisPoint in pointList)
            {
                var testRect = new RectangleF(thisPoint, testSize);
                ImageHelpers.DrawCube(MainGroup, assembly, testRect);
            }
            pointList = ImageHelpers.GetPoints(EnumShapes.Stars, DeckObject.HowManyStars, otherLocation, false, firstWidth);
            foreach (var thisPoint in pointList)
            {
                
                var testRect = new RectangleF(thisPoint, testSize);
                ImageHelpers.DrawStar(MainGroup, testRect);
            }
        }
    }
}