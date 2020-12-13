using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using SorryCardGameCP.Cards;
using SorryCardGameCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using System.Linq;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace SorryCardGameBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<SorryCardGameCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(66, 80);
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.LightBlue;
            }
            else
            {
                FillColor = cc.White;
            }
            base.BeforeFilling();
        }
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            if (DeckObject.Sorry == EnumSorry.Blank || DeckObject.Category == EnumCategory.Blank)
            {
                return false;
            }
            if (DeckObject.Sorry == EnumSorry.OnBoard)
            {
                if (DeckObject.Color == cc.Transparent || DeckObject.Color == "")
                {
                    return false;
                }
                if (DeckObject.Category == EnumCategory.Home || DeckObject.Category == EnumCategory.Start)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        private void DrawText(string content, RectangleF bounds, string color, float fontSize, bool bold = false, bool hasBorders = false)
        {
            Text text = new Text();
            text.Content = content;
            text.CenterText(MainGroup!, bounds);
            text.Font_Size = fontSize;
            text.Fill = color.ToWebColor();
            if (bold)
            {
                text.Font_Weight = "bold";
            }
            if (hasBorders)
            {
                text.PopulateStrokesToStyles();
            }
        }
        protected override void DrawBacks()
        {

            var firstRect = new RectangleF(3, 3, 60, 19);
            var fontSize = firstRect.Height * 0.4f;
            var secondRect = new RectangleF(3, 22, 60, 19);
            var thirdRect = new RectangleF(3, 41, 60, 19);
            var fourthRect = new RectangleF(3, 60, 60, 19);
            DrawText("Sorry!", firstRect, cc.Black, fontSize, true);
            DrawText("Revenge", secondRect, cc.Black, fontSize, true);
            DrawText("Card", thirdRect, cc.Black, fontSize, true);
            DrawText("Game", fourthRect, cc.Black, fontSize, true);
        }
        protected override void DrawImage()
        {
            if (DeckObject!.Sorry == EnumSorry.OnBoard)
            {
                DrawOnBoardCards();
                DrawHighlighters();
                return;
            }
            if (DeckObject!.Category == EnumCategory.Regular)
            {
                DrawPawn();
                DrawHighlighters();
                return;
            }
            if (DeckObject.Category == EnumCategory.Sorry)
            {
                DrawSorry();
                DrawHighlighters();
                return;
            }
            CustomBasicList<string> thisList;
            switch (DeckObject.Category)
            {
                case EnumCategory.Play2:
                    {
                        thisList = new CustomBasicList<string>() { "Play 2", "numbers", "to help", "you reach", "21!" };
                        break;
                    }

                case EnumCategory.Slide:
                    {
                        thisList = new CustomBasicList<string>() { DeckObject.Value.ToString(), "", "SLIDE" };
                        break;
                    }

                case EnumCategory.Switch:
                    {
                        thisList = new CustomBasicList<string>() { "Switch", "", "Direction" };
                        break;
                    }

                case EnumCategory.Take2:
                    {
                        thisList = new CustomBasicList<string>() { "Take 2", "cards to", "add to", "your", "Hand!" };
                        break;
                    }

                default:
                    {
                        return;
                    }
            }
            DrawMisc(thisList);
            DrawHighlighters();
        }
        private void DrawMisc(CustomBasicList<string> list)
        {
            if (DeckObject == null)
            {
                return;
            }
            RectangleF firstRect;
            RectangleF secondRect;
            RectangleF thirdRect = default;
            var tempRect = new RectangleF(6, 6, 50, 63);
            DrawRoundedRectangle(tempRect, cc.Yellow);
            if (list.Count == 3)
            {
                if (DeckObject.Category == EnumCategory.Slide)
                {
                    firstRect = new RectangleF(3, 15, 60, 30);
                    secondRect = new RectangleF(3, 40, 60, 30);
                }
                else
                {
                    firstRect = new RectangleF(3, 5, 60, 26);
                    secondRect = new RectangleF(3, 27, 60, 26);
                    thirdRect = new RectangleF(3, 47, 60, 26);
                }
            }
            else
            {
                if (DeckObject.Category == EnumCategory.Slide)
                {
                    return;
                }
                firstRect = new RectangleF(3, 5, 60, 15);
                secondRect = new RectangleF(3, 18, 60, 15);
                thirdRect = new RectangleF(3, 31, 60, 15);
            }
            var fontSize = firstRect.Height * 0.48f; // can always be adjusted as needed
            if (list.Count == 5)
            {
                fontSize = firstRect.Height * 0.7f;
            }
            string color = cc.DarkGreen;
            if (DeckObject.Category == EnumCategory.Slide)
            {
                fontSize = firstRect.Height * 0.62f;
                color = cc.White;
                DrawText(list.First(), firstRect, color, fontSize, hasBorders: true);
                DrawText(list.Last(), secondRect, color, fontSize, hasBorders: true);
            }
            else
            {
                DrawText(list.First(), firstRect, color, fontSize, hasBorders: false);

                if (!string.IsNullOrEmpty(list[1]))
                {
                    DrawText(list[1], secondRect, color, fontSize, hasBorders: false);
                }
                DrawText(list[2], thirdRect, color, fontSize, hasBorders: false);
            }
            if (list.Count == 3)
            {
                return;
            }
            firstRect = new RectangleF(3, 44, 60, 15);
            secondRect = new RectangleF(3, 57, 60, 15);
            DrawText(list[3], firstRect, color, fontSize, hasBorders: false);
            DrawText(list[4], secondRect, color, fontSize, hasBorders: false);
        }
        private void DrawSorry()
        {
            if (DeckObject == null)
            {
                return;
            }
            var tempRect = new RectangleF(6, 6, 50, 63);
            RectangleF firstRect;
            RectangleF secondRect;
            RectangleF thirdRect;
            if (DeckObject.Sorry == EnumSorry.Dont)
            {
                DrawRoundedRectangle(tempRect, cc.Blue);
                firstRect = new RectangleF(3, 3, 60, 26);
                secondRect = new RectangleF(3, 24, 60, 26);
                thirdRect = new RectangleF(3, 45, 60, 26);
                var fontSize = firstRect.Height * 0.6f;
                string color = cc.White;
                DrawText("Don't", firstRect, color, fontSize, true);
                DrawText("Be", secondRect, color, fontSize, true);
                DrawText("Sorry", thirdRect, color, fontSize, true);
                return;
            }
            CustomBasicList<string> otherList;
            if (DeckObject.Sorry == EnumSorry.At21)
            {
                DrawRoundedRectangle(tempRect, cc.Red);
                otherList = new CustomBasicList<string>() { "Play When", "Opponent", "Reaches 21!" };
            }
            else if (DeckObject.Sorry == EnumSorry.Regular)
            {
                DrawRoundedRectangle(tempRect, cc.Gray);
                otherList = new CustomBasicList<string>() { "Flip Another", "Player Back", "To Start" };
            }
            else
            {
                return;
            }
            RectangleF fourthRect;
            firstRect = new RectangleF(1, 3, 60, 21);
            secondRect = new RectangleF(1, 24, 60, 17);
            thirdRect = new RectangleF(1, 39, 60, 17);
            fourthRect = new RectangleF(1, 54, 60, 17);
            var firstFont = firstRect.Height * 0.8f;
            var secondFont = secondRect.Height * 0.5f;
            string firstColor = cc.Black;
            string secondColor = cc.White;
            DrawText("Sorry!", firstRect, firstColor, firstFont);
            DrawText(otherList.First(), secondRect, secondColor, secondFont);
            DrawText(otherList[1], thirdRect, secondColor, secondFont);
            DrawText(otherList.Last(), fourthRect, secondColor, secondFont);
        }
        private void DrawOnBoardCards()
        {
            var tempRect = new RectangleF(6, 6, 50, 63);
            DrawRoundedRectangle(tempRect, cc.LawnGreen);

            var pawnRect = new RectangleF(7, 7, 45, 45);
            MainGroup!.DrawPawnPiece(pawnRect, DeckObject!.Color);

            string text;
            if (DeckObject.Category == EnumCategory.Home)
            {
                text = "Home";
            }
            else if (DeckObject.Category == EnumCategory.Start)
            {
                text = "Start";
            }
            else
            {
                return;
            }
            var thisRect = new RectangleF(2, 47, 60, 28);
            var fontSize = thisRect.Height * .65f;
            DrawText(text, thisRect, cc.Black, fontSize, true);
        }
        private void DrawPawn()
        {
            var rect_Card = new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
            var tempRect = new RectangleF(6, 6, 50, 63);
            DrawRoundedRectangle(tempRect, cc.LawnGreen);
            var otherRect = new RectangleF(8, 12, 45, 45);
            MainGroup!.DrawPawnPiece(otherRect, cc.Black);
            var fontSize = rect_Card.Height * .5f;
            DrawText(DeckObject!.Value.ToString(), rect_Card, cc.White, fontSize, true, true);
        }
        private void DrawRoundedRectangle(RectangleF bounds, string color)
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(bounds);
            rect.Fill = color.ToWebColor();
            rect.RX = "3";
            rect.RY = "3";
            MainGroup!.Children.Add(rect);
        }
    }
}