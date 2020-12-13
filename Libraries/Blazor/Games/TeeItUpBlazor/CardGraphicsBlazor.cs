using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using TeeItUpCP.Cards;
using System.Drawing;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using SvgHelper.Blazor.Logic;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Linq;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace TeeItUpBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<TeeItUpCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(76, 105);
        protected override bool NeedsToDrawBacks => true;
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Green;
            }
            else
            {
                FillColor = cc.White;
            }
            base.BeforeFilling();
        }
        protected override bool CanStartDrawing()
        {
            return true;
        }
        private void DrawText(string content, RectangleF bounds, string color, float fontSize, bool hasBorders)
        {
            Text text = new Text();
            text.Content = content;
            text.CenterText(MainGroup!, bounds);
            text.Fill = color.ToWebColor();
            text.Font_Size = fontSize;
            if (hasBorders)
            {
                text.PopulateStrokesToStyles();
            }
        }
        protected override void DrawBacks()
        {
            RectangleF firstRect;
            RectangleF secondRect;
            RectangleF thirdRect;
            firstRect = new RectangleF(2, 2, 74, 33);
            secondRect = new RectangleF(2, 35, 74, 33);
            thirdRect = new RectangleF(2, 68, 74, 33);
            var fontSize = firstRect.Height * 0.6f; // can be adjusted
            string color = cc.White;
            DrawText("Tee", firstRect, color, fontSize, true);
            DrawText("It", secondRect, color, fontSize, true);
            DrawText("Up", thirdRect, color, fontSize, true);
        }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            var topRect = new RectangleF(13, 13, 50, 50);
            DrawCircle(topRect);
            if (DeckObject.IsMulligan == false)
            {
                var firstFonts = topRect.Height * 0.8f;
                DrawText(DeckObject.Points.ToString(), topRect, cc.White, firstFonts, true);
            }
            var fullBottom = new RectangleF(4, 67, 68, 35);
            var firstBottom = new RectangleF(4, 67, 68, 17);
            var secondBottom = new RectangleF(4, 84, 68, 17);
            var thisList = GetTextList();
            var fontSize = fullBottom.Height * 0.41f;
            string color;
            if (DeckObject.IsMulligan)
            {
                color = cc.Red;
            }
            else
            {
                color = cc.Green;
            }

            if (thisList.Count == 1)
            {
                DrawText(thisList.Single(), fullBottom, color, fontSize, false);
            }
            else if (thisList.Count == 2)
            {
                DrawText(thisList.First(), firstBottom, color, fontSize, false);
                DrawText(thisList.Last(), secondBottom, color, fontSize, false);
            }
        }
        private void DrawCircle(RectangleF bounds)
        {
            if (MainGroup == null)
            {
                return;
            }
            Circle circle = new Circle();
            circle.PopulateCircle(bounds, cc.Green);
            MainGroup.Children.Add(circle);
        }
        private CustomBasicList<string> GetSingleList(string firstText)
        {
            return new CustomBasicList<string>() { firstText };
        }
        private CustomBasicList<string> GetPairList(string firstText, string secondText)
        {
            return new CustomBasicList<string>() { firstText, secondText };
        }
        private CustomBasicList<string> GetTextList()
        {
            if (DeckObject == null)
            {
                return new CustomBasicList<string>();
            }
            if (DeckObject.IsMulligan == true)
            {
                return GetSingleList("Mulligan");
            }
            if (DeckObject.Points == -5)
            {
                return GetPairList("Hole", "In One");
            }
            if (DeckObject.Points == -3)
            {
                return new CustomBasicList<string>() { "Albatross" };
            }
            if (DeckObject.Points == -2)
            {
                return new CustomBasicList<string>() { "Eagle" };
            }
            if (DeckObject.Points == -1)
            {
                return GetSingleList("Birdie");
            }
            if (DeckObject.Points == 0)
            {
                return GetSingleList("Par");
            }
            if (DeckObject.Points == 1)
            {
                return GetSingleList("Bogey");
            }
            if (DeckObject.Points == 2)
            {
                return GetPairList("Double", "Bogey");
            }
            if (DeckObject.Points == 3)
            {
                return GetPairList("Triple", "Bogey");
            }
            if (DeckObject.Points == 4)
            {
                return GetPairList("Out Of", "Bounds");
            }
            if (DeckObject.Points == 5)
            {
                return GetPairList("Water", "Hazard");
            }
            if (DeckObject.Points == 6)
            {
                return GetPairList("Sand", "Trap");
            }
            if (DeckObject.Points == 7)
            {
                return GetSingleList("In Rough");
            }
            if (DeckObject.Points == 8)
            {
                return GetPairList("Lost", "Ball");
            }
            if (DeckObject.Points == 9)
            {
                return GetSingleList("In Ravine");
            }
            return new CustomBasicList<string>(); //since runtime messages don't show up anyways (?)
        }
    }
}