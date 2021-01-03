using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Collections;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Cards
{


    public class DeckOfCardsImageBlazor<R> : BaseDeckGraphics<R>
        where R : class, IRegularCard, new()
    {

        [Parameter]
        public bool ExtraSolitaireSuit { get; set; }
        protected override SizeF DefaultSize { get; } = new SizeF(165, 216); //decided to double the default size.
        protected override bool NeedsToDrawBacks { get; } = true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject == null)
            {
                return false; //we don't even have one.
            }
            if (DeckObject.Suit == EnumSuitList.None && DeckObject.CardType == EnumRegularCardTypeList.None && DeckObject.IsUnknown == false && EmptyBorders == false)
            {
                return false;
            }
            return true;
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
        private EnumSuitList SuitToDisplay
        {
            get
            {
                if (DeckObject!.DisplaySuit != EnumSuitList.None)
                {
                    return DeckObject.DisplaySuit;
                }
                return DeckObject.Suit;
            }
        }

        private EnumRegularCardValueList ValueToDisplay
        {
            get
            {
                if (DeckObject!.DisplayNumber == EnumRegularCardValueList.None)
                {
                    return DeckObject.Value;
                }
                return DeckObject.DisplayNumber;
            }
        }

        private string GetTextOfCard()
        {
            EnumRegularCardValueList tempValue = ValueToDisplay;
            switch (tempValue)
            {
                case EnumRegularCardValueList.LowAce:
                case EnumRegularCardValueList.HighAce:
                    {
                        return "A";
                    }
                case EnumRegularCardValueList.Jack:
                    {
                        return "J";
                    }
                case EnumRegularCardValueList.Queen:
                    {
                        return "Q";
                    }
                case EnumRegularCardValueList.King:
                    {
                        return "K";
                    }
                default:
                    {
                        return tempValue.FromEnum().ToString();
                    }
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
            string color;
            EnumSuitList suit;
            suit = SuitToDisplay;
            if (suit == EnumSuitList.Clubs || suit == EnumSuitList.Spades)
            {
                color = cc.Black;
            }
            else
            {
                color = cc.Red;
            }
            text.Fill = color.ToWebColor(); //try this.
            MainGroup!.Children.Add(text);

            MainGroup.DrawCardSuit(suit, 3, 46, _suitSize, _suitSize, color);

            if (ExtraSolitaireSuit)
            {
                MainGroup.DrawCardSuit(suit, 123, 5, _suitSize, _suitSize, color);
            }
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
            other.Children.Add(text);
            other.DrawCardSuit(suit, 5, 46, _suitSize, _suitSize, color);
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
            EnumRegularCardValueList tempValue = ValueToDisplay;
            switch (tempValue)
            {
                case EnumRegularCardValueList.LowAce:
                case EnumRegularCardValueList.HighAce:
                    {
                        arr_Current.Add(new PointF(2, 4));
                        break;
                    }
                case EnumRegularCardValueList.Two:
                    {
                        arr_Current.Add(new PointF(2, 1));
                        arr_Current.Add(new PointF(2, 7));
                        break;
                    }
                case EnumRegularCardValueList.Three:
                    {
                        arr_Current.Add(new PointF(2, 1));
                        arr_Current.Add(new PointF(2, 4));
                        arr_Current.Add(new PointF(2, 7));
                        break;
                    }
                case EnumRegularCardValueList.Four:
                    {
                        arr_Current.Add(new PointF(1, 1));
                        arr_Current.Add(new PointF(1, 7));
                        arr_Current.Add(new PointF(3, 1));
                        arr_Current.Add(new PointF(3, 7));
                        break;
                    }
                case EnumRegularCardValueList.Five:
                    {
                        arr_Current.Add(new PointF(1, 1));
                        arr_Current.Add(new PointF(1, 7));
                        arr_Current.Add(new PointF(3, 1));
                        arr_Current.Add(new PointF(3, 7));
                        arr_Current.Add(new PointF(2, 4));
                        break;
                    }
                case EnumRegularCardValueList.Six:
                    {
                        arr_Current.Add(new PointF(1, 1));
                        arr_Current.Add(new PointF(1, 4));
                        arr_Current.Add(new PointF(1, 7));
                        arr_Current.Add(new PointF(3, 1));
                        arr_Current.Add(new PointF(3, 4));
                        arr_Current.Add(new PointF(3, 7));
                        break;
                    }
                case EnumRegularCardValueList.Seven:
                    {
                        arr_Current.Add(new PointF(1, 1));
                        arr_Current.Add(new PointF(1, 4));
                        arr_Current.Add(new PointF(1, 7));
                        arr_Current.Add(new PointF(3, 1));
                        arr_Current.Add(new PointF(3, 4));
                        arr_Current.Add(new PointF(3, 7));
                        arr_Current.Add(new PointF(2, 3));
                        break;
                    }
                case EnumRegularCardValueList.Eight:
                    {
                        arr_Current.Add(new PointF(1, 1));
                        arr_Current.Add(new PointF(1, 3));
                        arr_Current.Add(new PointF(1, 5));
                        arr_Current.Add(new PointF(1, 7));
                        arr_Current.Add(new PointF(3, 1));
                        arr_Current.Add(new PointF(3, 3));
                        arr_Current.Add(new PointF(3, 5));
                        arr_Current.Add(new PointF(3, 7));
                        break;
                    }
                case EnumRegularCardValueList.Nine:
                    {
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
                    }
                case EnumRegularCardValueList.Ten:
                    {
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
                    }
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
            EnumSuitList suitName = SuitToDisplay;
            string color;
            if (suitName == EnumSuitList.Clubs || suitName == EnumSuitList.Spades)
            {
                color = cc.Black;
            }
            else
            {
                color = cc.Red;
            }

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
            currentGroup.DrawCardSuit(suitName, rect_Suit, color);
        }

        private void DrawFaceCards()
        {
            var suit = SuitToDisplay;
            string color;
            if (suit == EnumSuitList.Clubs || suit == EnumSuitList.Spades)
            {
                color = cc.Black;
            }
            else
            {
                color = cc.Red;
            }
            RectangleF rect = new RectangleF(40, 60, 75, 75);
            //next step is the crown part.
            MainGroup!.DrawRoyalSuits(rect, color);
            //hopefully this simple this time.
            
            //MainGroup!.DrawCardSuit(suit, rect, color);
        }
        private readonly float _suitSize = 32.4f;
        protected override void DrawImage()
        {
            var rect_Center = new RectangleF(40, 27, 80, 162);
            if (DeckObject!.CardType == EnumRegularCardTypeList.Regular)
            {
                DrawCardAndStartingSuit();
                var value = ValueToDisplay;
                if (value > EnumRegularCardValueList.Ten && value != EnumRegularCardValueList.HighAce)
                {
                    DrawFaceCards();
                }
                else
                {
                    DrawCenterSuits(rect_Center);
                }
            }
            else
            {
                DrawIrregularCards();
            }
        }
        private void DrawIrregularCards()
        {
            string clr_Suit;
            if (DeckObject!.Color == EnumRegularColorList.Black)
            {
                clr_Suit = cc.Black;
            }
            else
            {
                clr_Suit = cc.Red;
            }
            if (DeckObject.CardType == EnumRegularCardTypeList.Joker)
            {
                RectangleF firstRect;
                RectangleF secondRect;
                RectangleF middleRect;
                middleRect = new RectangleF(4, 70, 160, 70);
                var fontSize = middleRect.Height * 0.8f; // can adjust as needed
                firstRect = new RectangleF(50, 6, 70, 70);
                secondRect = new RectangleF(50, 130, 70, 70);
                string textColor;
                string tempEye;
                string tempSolid;
                string tempBorder;
                textColor = clr_Suit;
                tempSolid = clr_Suit; //this too.
                if (clr_Suit == cc.Black)
                {
                    tempBorder = cc.Aqua;
                }
                else
                {
                    tempBorder = cc.Black;
                }
                tempEye = tempBorder;
                MainGroup!.DrawSmiley(firstRect, tempSolid, tempBorder, tempEye, 4);
                MainGroup!.DrawSmiley(secondRect, tempSolid, tempBorder, tempEye, 4);
                MainGroup!.DrawCenteredText(middleRect, fontSize, "Joker", textColor);
                return;
            }
            Ellipse ellipse;
            if (DeckObject.CardType == EnumRegularCardTypeList.Continue)
            {
                ellipse = new Ellipse();
                ellipse.PopulateStrokesToStyles(cc.Green.ToWebColor(), 8.25f);
                ellipse.CX = "82.5";
                ellipse.CY = "108";
                ellipse.RX = "41.25";
                ellipse.RY = "41.25";
                MainGroup!.Children.Add(ellipse);
                return;
            }
            if (DeckObject.CardType == EnumRegularCardTypeList.Stop)
            {
                Line line = new Line();
                MainGroup!.Children.Add(line);
                line.X1 = "41.25";
                line.Y1 = "66.75";
                line.X2 = "123.75";
                line.Y2 = "149.25";
                line.PopulateStrokesToStyles(cc.Red.ToWebColor(), 8.25f);
                line = new Line();
                MainGroup.Children.Add(line);
                line.X1 = "41.25";
                line.Y1 = "149.25";
                line.X2 = "123.75";
                line.Y2 = "66.75";
                line.PopulateStrokesToStyles(cc.Red.ToWebColor(), 8.25f);
            }
        }
    }
}