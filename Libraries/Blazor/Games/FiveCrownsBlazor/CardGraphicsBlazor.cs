using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Collections;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace FiveCrownsBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<FiveCrownsCardInformation>
    {

        private readonly float _suitSize = 32.4f;
        protected override SizeF DefaultSize { get; } = new SizeF(165, 216);
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
            image.Width = "140";
            image.Height = "191";
            image.X = "10";
            image.Y = "10";
            MainGroup!.Children.Add(image);
        }
        protected override void DrawImage()
        {
            var rect_Center = new RectangleF(40, 27, 80, 162);
            if (DeckObject!.CardValue == EnumCardValueList.Joker)
            {
                DrawJoker(); //this part is the same.
                return;
            }
            DrawCardAndStartingSuit();
            if (DeckObject.CardValue > EnumCardValueList.Ten)
            {
                DrawFaceCards();
            }
            else
            {
                DrawCenterSuits(rect_Center);
            }
        }

        private void DrawCenterSuits(RectangleF rect_Center)
        {
            Hashtable arr_Rectangles = new Hashtable(); // hashtable is supported with the .net standard 2.0.  collection is not
            CustomBasicList<PointF> arr_Current = new CustomBasicList<PointF>();
            int int_Row;
            int int_Col;
            int int_Count;
            RectangleF rect_Temp;
            int int_Height;
            PointF pt_Temp;
            int_Height = (int)rect_Center.Height / 5;
            for (int_Row = 0; int_Row <= 6; int_Row++)
            {
                for (int_Col = 0; int_Col <= 2; int_Col++)
                {
                    rect_Temp = new RectangleF(rect_Center.Left + ((rect_Center.Width / 3) * int_Col), rect_Center.Top + ((rect_Center.Height / 7) * int_Row) - (int_Height - (rect_Center.Height / 7)), _suitSize, _suitSize);
                    arr_Rectangles.Add(new PointF(int_Col + 1, int_Row + 1), rect_Temp);
                }
            }
            switch (DeckObject!.CardValue)
            {
                case EnumCardValueList.Three:
                    arr_Current.Add(new PointF(2, 1));
                    arr_Current.Add(new PointF(2, 4));
                    arr_Current.Add(new PointF(2, 7));
                    break;
                case EnumCardValueList.Four:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 7));
                    break;
                case EnumCardValueList.Five:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 7));
                    arr_Current.Add(new PointF(2, 4));
                    break;
                case EnumCardValueList.Six:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 4));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 4));
                    arr_Current.Add(new PointF(3, 7));
                    break;
                case EnumCardValueList.Seven:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 4));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 4));
                    arr_Current.Add(new PointF(3, 7));
                    arr_Current.Add(new PointF(2, 3));
                    break;
                case EnumCardValueList.Eight:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 3));
                    arr_Current.Add(new PointF(1, 5));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 3));
                    arr_Current.Add(new PointF(3, 5));
                    arr_Current.Add(new PointF(3, 7));
                    break;
                case EnumCardValueList.Nine:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 3));
                    arr_Current.Add(new PointF(1, 5));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 3));
                    arr_Current.Add(new PointF(3, 5));
                    arr_Current.Add(new PointF(3, 7));
                    arr_Current.Add(new PointF(2, 4));
                    break;
                case EnumCardValueList.Ten:
                    arr_Current.Add(new PointF(1, 1));
                    arr_Current.Add(new PointF(1, 3));
                    arr_Current.Add(new PointF(1, 5));
                    arr_Current.Add(new PointF(1, 7));
                    arr_Current.Add(new PointF(3, 1));
                    arr_Current.Add(new PointF(3, 3));
                    arr_Current.Add(new PointF(3, 5));
                    arr_Current.Add(new PointF(3, 7));
                    arr_Current.Add(new PointF(2, 2));
                    arr_Current.Add(new PointF(2, 6));
                    break;
                default:
                    break;
            }

            var loopTo = arr_Current.Count;
            for (int_Count = 1; int_Count <= loopTo; int_Count++)
            {
                pt_Temp = arr_Current[int_Count - 1]; // because its 0 based
                rect_Temp = (RectangleF)arr_Rectangles[pt_Temp]!;
                if (pt_Temp.Y > 4)
                {
                    DrawSuit(rect_Temp, true);
                }
                else
                {
                    DrawSuit(rect_Temp, false);
                }
            }
        }

        private void DrawSuit(RectangleF rect_Suit, bool bln_Flip)
        {
            G currentGroup;
            if (bln_Flip == false)
            {
                currentGroup = MainGroup!;
            }
            else
            {
                currentGroup = new G(); //rotations
                currentGroup.Rotate180Degrees(rect_Suit);
                MainGroup!.Children.Add(currentGroup);
            }
            DrawCustomSuit(currentGroup, GetColor, rect_Suit);
        }

        private void DrawFaceCards()
        {
            string color = GetColor;
            RectangleF rect = new RectangleF(40, 60, 75, 75);
            if (color != cc.Yellow)
            {
                MainGroup!.DrawRoyalSuits(rect, color);
            }
            else
            {
                MainGroup!.DrawRoyalSuits(rect, color, cc.Black, 20);
            }
        }

        private void DrawCardAndStartingSuit()
        {
            Text text = new Text();
            string value = GetTextOfCard();
            text.Content = value;
            text.PopulateTextFont();
            double fontSize;
            string x;
            if (value == "10")
            {
                fontSize = 36;
                x = "0";
            }
            else
            {
                fontSize = 43.2;
                x = "5";
            }

            text.Font_Size = fontSize;
            text.X = x;
            text.Y = "43";
            string color = GetColor;
            text.Fill = color.ToWebColor(); //try this.
            if (text.Fill == cc.Yellow.ToWebColor())
            {
                text.PopulateStrokesToStyles(strokeWidth: 2); //try this way.
            }
            MainGroup!.Children.Add(text);
            DrawCustomSuit(MainGroup, color, 3);
            G other = new G();
            other.Transform = "rotate(180) translate(-163, -214)";
            MainGroup.Children.Add(other);
            text = new Text();
            text.Content = value;
            text.PopulateTextFont();
            text.Font_Size = fontSize;
            text.X = x;
            text.Y = "43";
            text.Fill = color.ToWebColor();
            if (text.Fill == cc.Yellow.ToWebColor())
            {
                text.PopulateStrokesToStyles(strokeWidth: 2);
            }
            other.Children.Add(text);
            DrawCustomSuit(other, color, 5);
        }
        private void DrawCustomSuit(G group, string color, int starts)
        {
            RectangleF bounds = new RectangleF(starts, 46, _suitSize, _suitSize);
            DrawCustomSuit(group, color, bounds);
        }
        private void DrawCustomSuit(G group, string color, RectangleF bounds)
        {
            if (DeckObject!.Suit == EnumSuitList.Clubs)
            {
                group.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Clubs, bounds, color);
            }
            else if (DeckObject.Suit == EnumSuitList.Hearts)
            {
                group.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Hearts, bounds, color);
            }
            else if (DeckObject.Suit == EnumSuitList.Diamonds)
            {
                group.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Diamonds, bounds, color);
            }
            else if (DeckObject.Suit == EnumSuitList.Spades)
            {
                group.DrawCardSuit(BasicGameFrameworkLibrary.RegularDeckOfCards.EnumSuitList.Spades, bounds, color);
            }
            else if (DeckObject.Suit == EnumSuitList.Stars)
            {
                group.DrawStar(bounds, color, cc.Black, 3); //i think
            }
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
        
        private void DrawJoker()
        {
            if (MainGroup == null)
            {
                return;
            }
            RectangleF firstRect = new RectangleF(0, 5, 160, 55);
            var fontSize = firstRect.Height;
            Text text = new Text();
            text.Fill = cc.Black.ToWebColor();
            text.Content = "Joker";
            text.Font_Size = fontSize;
            text.CenterText(MainGroup, firstRect);
            RectangleF secondRect = new RectangleF(30, 75, 100, 100);
            MainGroup.DrawSmiley(secondRect, "", cc.Black, cc.Black, 2);
        }
    }
}