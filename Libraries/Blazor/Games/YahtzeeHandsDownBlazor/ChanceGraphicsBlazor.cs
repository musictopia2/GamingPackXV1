using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using System.Drawing;
using YahtzeeHandsDownCP.Cards;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace YahtzeeHandsDownBlazor
{
    public class ChanceGraphicsBlazor : BaseDeckGraphics<ChanceCardInfo>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => false;
        protected override bool CanStartDrawing()
        {
            return DeckObject!.Points > 0;
        }
        protected override void DrawBacks() { }
        protected override void DrawImage()
        {
            if (MainGroup == null || DeckObject == null)
            {
                return;
            }
            if (DeckObject.Points > 7 || DeckObject.Points < 1)
            {
                return;
            }
            if (DeckObject.Points == 4 || DeckObject.Points == 6)
            {
                return;
            }
            var tempSize = 4;
            var rect_Card = new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
            var tempRect = new RectangleF(rect_Card.Location.X + tempSize, rect_Card.Location.Y + tempSize, rect_Card.Width - (tempSize * 3), rect_Card.Height - (tempSize * 3));
            MainGroup.DrawNormalRectangle(tempRect, cc.DarkSlateBlue);
            var firstRect = new RectangleF(6, 12, 40, 40);
            MainGroup.DrawNormalRectangle(firstRect, cc.White, 2);
            firstRect = new RectangleF(5, 12, 40, 30);
            var fontSize = 25;
            MainGroup.DrawCustomText(firstRect, DeckObject.Points.ToString(), fontSize, cc.Black);
            var secondRect = new RectangleF(1, 34, 50, 20);
            fontSize = 14;
            string thisText;
            if (DeckObject.Points == 1)
            {
                thisText = "Point";
            }
            else
            {
                fontSize = 12;
                thisText = "Points";
            }
            MainGroup.DrawCustomText(secondRect, thisText, fontSize, cc.Black, true);
        }
    }
}