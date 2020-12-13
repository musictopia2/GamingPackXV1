using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace FiveCrownsBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<FiveCrownsCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject == null)
            {
                return false; //we don't even have one.
            }
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            if (DeckObject.CardType == EnumCardTypeList.Joker)
            {
                return true;
            }
            return DeckObject!.Suit != EnumSuitList.None;
        }
        protected override void DrawBacks()
        {
            Image image = new Image();
            image.PopulateFullExternalImage(this, "deckofcardback.svg");
            image.X = "5";
            image.Y = "5";
            image.Height = "57";
            image.Width = "40";
            MainGroup!.Children.Add(image);
        }
        protected override void DrawImage()
        {
            if (DeckObject!.CardValue == EnumCardValueList.Joker)
            {
                DrawJoker();
                return;
            }
            RectangleF rect_card = new RectangleF(7, 5, 40, 40);
            DrawSuit(rect_card);
            rect_card = new RectangleF(0, 42, 55, 28);
            DrawValue(rect_card);
        }
        private string GetColor
        {
            get
            {
                return DeckObject!.Suit switch
                {
                    EnumSuitList.Clubs => cc.Green,
                    EnumSuitList.Diamonds => cc.Blue,
                    EnumSuitList.Spades => cc.Black,
                    EnumSuitList.Hearts => cc.Red,
                    EnumSuitList.Stars => cc.Yellow,
                    _ => "",
                };
            }
        }
        private void DrawValue(RectangleF rect_Card)
        {
            if (MainGroup == null)
            {
                return;
            }
            var fontSize = rect_Card.Height * .7;
            Text text = new Text();
            text.PopulateStrokesToStyles();
            text.Fill = GetColor.ToWebColor();
            text.Font_Size = fontSize;
            text.Content = GetTextOfCard();
            text.CenterText(MainGroup, rect_Card);
        }
        private string GetTextOfCard()
        {
            return DeckObject!.CardValue switch
            {
                EnumCardValueList.Jack => "J",
                EnumCardValueList.Queen => "Q",
                EnumCardValueList.King => "K",
                _ => DeckObject.CardValue.FromEnum().ToString()
            };
        }
        private void DrawSuit(RectangleF rect_Card)
        {
            if (MainGroup == null)
            {
                return;
            }
            string color = GetColor;
            switch (DeckObject!.Suit)
            {
                case EnumSuitList.Clubs:
                    MainGroup.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Clubs, rect_Card, color);
                    break;
                case EnumSuitList.Diamonds:
                    MainGroup.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Diamonds, rect_Card, color);
                    break;
                case EnumSuitList.Spades:
                    MainGroup.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Spades, rect_Card, color);
                    break;
                case EnumSuitList.Hearts:
                    MainGroup.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Hearts, rect_Card, color);
                    break;
                case EnumSuitList.Stars:
                    MainGroup.DrawStar(color, cc.Black, 1);
                    break;
                default:
                    break;
            }
        }
        private void DrawJoker()
        {
            if (MainGroup == null)
            {
                return;
            }
            RectangleF firstRect = new RectangleF(0, 0, 55, 30);
            var fontSize = firstRect.Height * .5;
            Text text = new Text();
            text.Fill = cc.Black.ToWebColor();
            text.Content = "Joker";
            text.Font_Size = fontSize;
            text.CenterText(MainGroup, firstRect);
            RectangleF secondRect = new RectangleF(8, 25, 35, 35);
            MainGroup.DrawSmiley(secondRect, "", cc.Black, cc.Black, 2);
        }
    }
}