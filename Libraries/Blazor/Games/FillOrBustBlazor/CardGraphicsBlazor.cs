using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using FillOrBustCP.Cards;
using FillOrBustCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace FillOrBustBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<FillOrBustCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(107, 135);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            return DeckObject!.Status != EnumCardStatusList.Unknown;
        }
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Red;
            }
            else if (DeckObject!.Value == 2500)
            {
                FillColor = cc.Maroon;
            }
            else if (DeckObject.Status == EnumCardStatusList.MustBust)
            {
                FillColor = cc.Red;
            }
            else if (DeckObject!.Value > 0)
            {
                FillColor = cc.Aqua;
            }
            else if (DeckObject.Status == EnumCardStatusList.DoubleTrouble)
            {
                FillColor = cc.Aqua;
            }
            else
            {
                FillColor = cc.Maroon;
            }
        }
        protected override void DrawBacks() { }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            if (DeckObject.Value == 0 && DeckObject.Status == EnumCardStatusList.None)
            {
                return;
            }
            if (DeckObject.Value == 2500)
            {
                DrawRevenge();
                return;
            }
            if (DeckObject.Value == 1000)
            {
                DrawFill();
                return;
            }
            if (DeckObject.Status == EnumCardStatusList.MustBust)
            {
                DrawMustBust();
                return;
            }
            if (DeckObject.Status == EnumCardStatusList.DoubleTrouble)
            {
                DrawDoubleTrouble();
                return;
            }
            if (DeckObject.Status == EnumCardStatusList.NoDice)
            {
                DrawNoDice();
                return;
            }
            if (DeckObject.Value > 0)
            {
                DrawBonus();
            }
        }
        private void DrawText(string content, float fontSize, string fillColor, RectangleF rect, bool hasBorders = true)
        {
            Text text = new Text();
            text.Content = content;
            
            text.CenterText(MainGroup!, rect);
            text.Fill = fillColor.ToWebColor();
            text.Font_Size = fontSize;
            if (hasBorders)
            {
                text.PopulateStrokesToStyles(); //i think.
            }
            else
            {
                text.Font_Weight = "bold";
            }
        }
        private void DrawRectangle(RectangleF rect, string fillcolor, bool hasBorders = false)
        {
            Rect output = new Rect();
            output.PopulateRectangle(rect);
            output.Fill = fillcolor.ToWebColor();
            if (hasBorders)
            {
                output.PopulateStrokesToStyles(strokeWidth: 5, color: cc.Aqua.ToWebColor());
            }
            MainGroup!.Children.Add(output);
        }
        private void DrawBonus()
        {
            var firstRect = new RectangleF(4, 4, 95, 25);
            var secondRect = new RectangleF(4, 100, 95, 25);
            DrawRectangle(firstRect, cc.Red);
            DrawRectangle(secondRect, cc.Red);
            var firstText = new RectangleF(0, 35, 107, 35);
            var secondText = new RectangleF(0, 70, 107, 35);
            var fontSize = 33;
            DrawText("Bonus", fontSize, cc.Maroon, firstText);
            DrawText(DeckObject!.Value.ToString(), fontSize, cc.Maroon, secondText);
        }
        private void DrawFill()
        {
            var firstText = new RectangleF(0, 20, 107, 40);

            var firstFont = 40;
            var secondFont = 35;
            var tempRect = new RectangleF(15, 65, 77, 35);
            DrawRectangle(tempRect, cc.Red);
            DrawText("Fill", firstFont, cc.Red, firstText);
            DrawText("1000", secondFont, cc.Aqua, tempRect);
        }
        private void DrawDoubleTrouble()
        {
            RectangleF firstRect;
            RectangleF lastRect;
            RectangleF firstText;
            RectangleF middleText;
            RectangleF lastText;
            firstRect = new RectangleF(4, 5, 95, 20);
            lastRect = new RectangleF(4, 107, 95, 20);
            firstText = new RectangleF(0, 22, 107, 30);
            middleText = new RectangleF(0, 52, 107, 30);
            lastText = new RectangleF(0, 82, 107, 30);
            string firstColor = cc.LightPink;
            string middleColor = cc.Maroon;
            var fontSize = 25;
            DrawRectangle(firstRect, cc.Red);
            DrawRectangle(lastRect, cc.Red);
            DrawText("Double", fontSize, firstColor, firstText);
            DrawText("Trouble", fontSize, middleColor, middleText);
            DrawText("Double", fontSize, firstColor, lastText);
        }
        private void DrawMustBust()
        {
            RectangleF firstRect = new RectangleF(7, 10, 90, 25);
            RectangleF lastRect = new RectangleF(7, 97, 90, 25);
            DrawRectangle(firstRect, cc.Maroon, true);
            DrawRectangle(lastRect, cc.Maroon, true);
            var fontSize = 30;
            var textRect = new RectangleF(0, 35, 107, 37);
            DrawText("Must", fontSize, cc.Aqua, textRect);
            textRect = new RectangleF(0, 63, 107, 37); //i think.
            DrawText("Bust!", fontSize, cc.Aqua, textRect);
        }
        private void DrawRevenge()
        {
            RectangleF tempRect = new RectangleF(-3, 5, 109, 27);
            var fontSize = 18;
            DrawText("Vengeance", fontSize, cc.Aqua, tempRect, false);
            tempRect = new RectangleF(-1, 27, 109, 27);
            DrawText("2500", fontSize, cc.Aqua, tempRect, false);
            tempRect = new RectangleF(4, 55, 95, 70);
            DrawRectangle(tempRect, cc.Red);
            Image image = new Image();
            image.PopulateFullExternalImage(this, "revenge.png");
            image.PopulateImagePositionings(tempRect);
            MainGroup!.Children.Add(image);
        }
        private void DrawNoDice()
        {
            var textRect = new RectangleF(0, 10, 107, 44);
            var fontSize = 27;
            DrawText("No Dice", fontSize, cc.Aqua, textRect);
            var lastRect = new RectangleF(23, 59, 60, 60);
            Circle circle = new Circle();
            circle.PopulateCircle(lastRect, "");
            circle.PopulateStrokesToStyles(strokeWidth: 7, color: cc.Red.ToWebColor());
            MainGroup!.Children.Add(circle);
            PointF point = new PointF(lastRect.Left + (lastRect.Size.Width / 4), lastRect.Top + (lastRect.Size.Height / 4));
            SizeF size = new SizeF(34, 34);
            RectangleF fins = new RectangleF(point, size);
            MainGroup.DrawDice(fins, 6);
            Line line = new Line();
            PointF firstPoint = new PointF(lastRect.Left + 10, lastRect.Top + 10);
            PointF lastPoint = new PointF(lastRect.Right - 10, lastRect.Bottom - 10);
            line.PopulateLine(firstPoint, lastPoint);
            line.PopulateStrokesToStyles(strokeWidth: 7, color: cc.Red.ToWebColor(), opacity: .5);
            MainGroup.Children.Add(line);
        }
    }
}