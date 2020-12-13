using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using A8RoundRummyCP.Cards;
using System.Drawing;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using SvgHelper.Blazor.Logic;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using A8RoundRummyCP.Data;
namespace A8RoundRummyBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<A8RoundRummyCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            return true;
        }
        protected override void DrawBacks()
        {
            var firstRect = new RectangleF(0, 1, 54, 23);
            var secondRect = new RectangleF(0, 22, 54, 23);
            var thirdRect = new RectangleF(0, 43, 54, 23);
            var fontSize = firstRect.Height * .5;
            DrawText(firstRect, "8", cc.Red, fontSize);
            DrawText(secondRect, "Round", cc.Red, fontSize);
            DrawText(thirdRect, "Rummy", cc.Red, fontSize);
        }
        private void DrawText(RectangleF rectangle, string content, string color, double fontSize)
        {
            Text text = new Text();
            text.Fill = color.ToWebColor();
            text.CenterText(MainGroup!, rectangle);
            text.Content = content;
            text.Font_Size = fontSize;
        }
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Blue;
            }
            else
            {
                FillColor = cc.White;
            }
            base.BeforeFilling();
        }
        protected override void DrawImage()
        {
            switch (DeckObject!.CardType)
            {
                case EnumCardType.Regular:
                    DrawRegular();
                    break;
                case EnumCardType.Wild:
                    DrawWild();
                    break;
                case EnumCardType.Reverse:
                    DrawReverse();
                    break;
                default:
                    break;
            }
        }
        private void DrawRegular()
        {
            string color = DeckObject!.Color.ToColor();
            var shapeRect = new RectangleF(13, 5, 30, 30);
            if (DeckObject!.Shape == EnumCardShape.Circle)
            {
                Circle circle = new Circle();
                circle.PopulateCircle(shapeRect, color);
                circle.PopulateStrokesToStyles(strokeWidth: 2);
                MainGroup!.Children.Add(circle);
            }
            else if (DeckObject.Shape == EnumCardShape.Square)
            {
                Rect rect = new Rect();
                rect.PopulateRectangle(shapeRect);
                rect.Fill = color.ToWebColor();
                rect.PopulateStrokesToStyles(strokeWidth: 2);
                MainGroup!.Children.Add(rect);
            }
            else
            {
                Polygon poly = shapeRect.PopulateTrianglePolygon(color);
                poly.PopulateStrokesToStyles(strokeWidth: 2);
                MainGroup!.Children.Add(poly);
            }
            var textRect = new RectangleF(0, 35, 55, 35);
            var fontSize = textRect.Height;
            Text text = new Text();
            text.CenterText(MainGroup, textRect);
            text.Font_Size = fontSize;
            text.Fill = color.ToWebColor();
            text.PopulateStrokesToStyles();
            text.Content = DeckObject.Value.ToString();
        }
        private void DrawWild()
        {
            var smileyRect = new RectangleF(8, 5, 35, 35);
            var textRect = new RectangleF(0, 40, 55, 30);
            var fontSize = textRect.Height * 0.5f;
            MainGroup!.DrawSmiley(smileyRect, "", cc.Black, cc.Black, 2);
            DrawText(textRect, "WILD", cc.Black, fontSize);
        }
        private void DrawReverse()
        {
            var firstRect = new RectangleF(0, 1, 54, 23);
            var secondRect = new RectangleF(0, 25, 54, 23);
            var thirdRect = new RectangleF(0, 49, 54, 23);
            var fontSize = firstRect.Height * .45;
            DrawText(firstRect, "Reverse", cc.Green, fontSize);
            DrawText(secondRect, "And New", cc.Green, fontSize);
            DrawText(thirdRect, "Hand", cc.Green, fontSize);
        }
    }
}