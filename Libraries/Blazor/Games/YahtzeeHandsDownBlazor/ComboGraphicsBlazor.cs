using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Drawing;
using System.Linq;
using YahtzeeHandsDownCP.Cards;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace YahtzeeHandsDownBlazor
{
    public class ComboGraphicsBlazor : BaseDeckGraphics<ComboCardInfo>
    {
        protected override SizeF DefaultSize => new SizeF(234, 130);
        protected override bool NeedsToDrawBacks => false;
        protected override bool CanStartDrawing()
        {
            return DeckObject != null;
        }
        protected override void DrawBacks() { }
        protected override void DrawImage()
        {
            if (MainGroup == null || DeckObject == null)
            {
                return;
            }
            var tempRect = new RectangleF(5, 9, 220, 113);
            MainGroup.DrawNormalRectangle(tempRect, cc.Red);
            var firstRect = new RectangleF(4, 4, 48, 48);
            MainGroup.DrawNormalRectangle(firstRect, cc.White);
            var fontSize = 14;
            MainGroup.DrawCustomText(firstRect, $"{DeckObject.Points} PTS.", fontSize, cc.Black);
            var secondRect = new RectangleF(54, 4, 170, 50);
            var temps = 3;
            MainGroup.DrawRoundedRectangle(secondRect, cc.Yellow, temps, 2);
            firstRect = new RectangleF(56, 4, 170, 16);
            fontSize = 15;
            MainGroup.DrawCustomText(firstRect, DeckObject.FirstDescription, fontSize, cc.Black, true);
            string firstText;
            string secondText;
            var tempList = DeckObject.SecondDescription.Split("|").ToCustomBasicList();
            if (tempList.Count != 2)
            {
                return;
            }
            firstText = tempList.First();
            secondText = tempList.Last();
            firstRect = new RectangleF(56, 20, 170, 16);
            MainGroup.DrawCustomText(firstRect, firstText, fontSize, cc.Black);
            secondRect = new RectangleF(56, 36, 170, 16);
            MainGroup.DrawCustomText(secondRect, secondText, fontSize, cc.Black);
            float diffs = 44;
            var finalSize = 70;
            float lefts;
            lefts = 0;
            var tops = 58;
            var otherSize = 42;
            foreach (var thisItem in DeckObject.SampleList)
            {
                RectangleF rect = new RectangleF(lefts, tops, otherSize, finalSize);
                CardGraphicsBlazor graphics = new CardGraphicsBlazor();
                YahtzeeHandsDownCardInformation card = new YahtzeeHandsDownCardInformation();
                card.Color = thisItem.Color;
                card.IsWild = thisItem.IsWild;
                card.FirstValue = thisItem.FirstValue;
                card.SecondValue = thisItem.SecondValue;
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                graphics.DeckObject = card;
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
                var results = graphics.GetGraphicsToEmbed(rect);
                MainGroup.Children.Add(results); //hopefully this simple (?)
                lefts += diffs;
            }
        }
    }
}