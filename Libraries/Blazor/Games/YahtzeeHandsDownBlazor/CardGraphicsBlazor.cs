using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using YahtzeeHandsDownCP.Cards;
using System.Drawing;
using YahtzeeHandsDownCP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace YahtzeeHandsDownBlazor
{
    public class CardGraphicsBlazor : BaseDarkCardsBlazor<YahtzeeHandsDownCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => true;

        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            return DeckObject.Color != EnumColor.None;
        }
        protected override bool IsLightColored => DeckObject!.Color == EnumColor.Yellow;
        protected override void DrawBacks()
        {
            if (MainGroup == null)
            {
                return;
            }
            var tempRect = new RectangleF(5, 5, DefaultSize.Width - 14, DefaultSize.Height - 14);
            MainGroup.DrawNormalRectangle(tempRect, cc.Red);
            var firstRect = new RectangleF(tempRect.Location.X, tempRect.Location.Y, tempRect.Width, tempRect.Height / 3);
            var secondRect = new RectangleF(tempRect.Location.X, firstRect.Bottom, tempRect.Width, firstRect.Height);
            var thirdRect = new RectangleF(tempRect.Location.X, secondRect.Bottom, tempRect.Width, secondRect.Height);
            var fontSize = firstRect.Height * 0.55f; // can experiment
            var color = cc.White;
            MainGroup.DrawCustomText(firstRect, "Yahtzee", fontSize, color);
            MainGroup.DrawCustomText(secondRect, "Hands", fontSize, color);
            MainGroup.DrawCustomText(thirdRect, "Down", fontSize, color);
        }
        private void DrawAllColors(RectangleF rect_Card)
        {
            var tempRect = GetStartingRect(rect_Card);
            var firstRect = new RectangleF(tempRect.Location.X, tempRect.Location.Y, tempRect.Width / 3, tempRect.Height);
            var secondRect = new RectangleF(firstRect.Right, tempRect.Location.Y, firstRect.Width, tempRect.Height);
            var thirdRect = new RectangleF(secondRect.Right, tempRect.Location.Y, secondRect.Width, tempRect.Height);
            if (MainGroup == null || DeckObject == null)
            {
                return;
            }
            MainGroup.DrawNormalRectangle(firstRect, cc.Red);
            MainGroup.DrawNormalRectangle(secondRect, cc.Blue);
            MainGroup.DrawNormalRectangle(thirdRect, cc.Yellow);
            var finalRect = GetCenterDiceRect();
            MainGroup.DrawDice(finalRect, DeckObject.FirstValue);
        }
        private void DrawWild(RectangleF rect_Card)
        {
            if (MainGroup == null || DeckObject == null)
            {
                return;
            }
            var tempRect = GetStartingRect(rect_Card);
            MainGroup.DrawNormalRectangle(tempRect, DeckObject.Color.ToColor());
            var otherRect = GetCenterDiceRect();
            var tempSize = 3;
            MainGroup.DrawRoundedRectangle(otherRect, cc.White, tempSize, 3);
            var fontSize = otherRect.Height * 0.35f; // can adjust as needed
            MainGroup.DrawCustomText(otherRect, "WILD", fontSize, cc.Black);
        }
        private void Draw2Numbers(RectangleF rect_Card)
        {
            var tempRect = GetStartingRect(rect_Card);
            MainGroup!.DrawNormalRectangle(tempRect, DeckObject!.Color.ToColor());
            tempRect = new RectangleF(11, 4, 30, 30);
            float width;
            float diffs = 0;
            if (tempRect.Height > tempRect.Width)
            {
                width = tempRect.Width;
                diffs = tempRect.Height - tempRect.Width;
            }
            else if (tempRect.Width > tempRect.Height)
            {
                width = tempRect.Height;
            }
            else
            {
                width = tempRect.Width;
            }
            var firstRect = new RectangleF(tempRect.Location.X, tempRect.Location.Y + (diffs / 2), width, width);
            tempRect = new RectangleF(11, 34, 30, 30);
            var secondRect = new RectangleF(tempRect.Location.X, tempRect.Location.Y, width, width);
            MainGroup!.DrawDice(firstRect, DeckObject.FirstValue);
            MainGroup!.DrawDice(secondRect, DeckObject.SecondValue);
        }
        private RectangleF GetCenterDiceRect()
        {
            var firstRect = new RectangleF(6, 13, 40, 40);
            float width;
            float diffs = 0;
            if (firstRect.Height > firstRect.Width)
            {
                width = firstRect.Width;
                diffs = firstRect.Height - firstRect.Width;
            }
            else if (firstRect.Width > firstRect.Width)
            {
                width = firstRect.Height;
            }
            else
            {
                width = firstRect.Width;
            }
            return new RectangleF(firstRect.Location.X, firstRect.Location.Y + (diffs / 2), width, width);
        }
        private RectangleF GetStartingRect(RectangleF rect_Card)
        {
            var fontSize = 4;
            var tempRect = new RectangleF(rect_Card.Location.X + fontSize, rect_Card.Location.Y + fontSize, rect_Card.Width - (fontSize * 3), rect_Card.Height - (fontSize * 3));
            return tempRect;
        }
        private RectangleF DefaultRectangle => new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
        protected override void DrawImage()
        {
            if (MainGroup == null || DeckObject == null)
            {
                return;
            }
            RectangleF rect_Card = DefaultRectangle;
            if (DeckObject.Color == EnumColor.Any)
            {
                DrawAllColors(rect_Card);
                return;
            }
            if (DeckObject.IsWild == true)
            {
                DrawWild(rect_Card);
                return;
            }
            if (DeckObject.SecondValue > 0)
            {
                Draw2Numbers(rect_Card);
                return;
            }
            var tempRect = GetStartingRect(rect_Card);
            MainGroup.DrawNormalRectangle(tempRect, DeckObject.Color.ToColor());
            var otherRect = GetCenterDiceRect();
            MainGroup.DrawDice(otherRect, DeckObject.FirstValue);
        }
    }
}