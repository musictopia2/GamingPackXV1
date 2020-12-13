using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using MonopolyCardGameCP.Cards;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MonopolyCardGameBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<MonopolyCardGameCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            return DeckObject!.IsUnknown == true || DeckObject.CardValue > 0;
        }
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Aqua;
            }
            else
            {
                FillColor = cc.White;
            }
            base.BeforeFilling();
        }
        protected override void DrawBacks() { } //try with no drawings because of rotated text.
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            switch (DeckObject.CardValue)
            {
                case int _ when DeckObject.CardValue < 7:
                    {
                        DrawTokenText();
                        DrawTokenImage();
                        break;
                    }
                case 7:
                    {
                        DrawMRMonopolyText();
                        DrawMrMonopolyImage();
                        break;
                    }
                case object _ when DeckObject.CardValue < 12:
                    {
                        DrawHouseText();
                        DrawHouseImage();
                        break;
                    }
                case 12:
                    {
                        DrawHotel();
                        break;
                    }
                case object _ when DeckObject.CardValue < 17:
                    {
                        DrawRailRoadText();
                        DrawRailroadImage();
                        break;
                    }
                case 17:
                    {
                        DrawUtilityText();
                        DrawElectricImage();
                        break;
                    }
                case 18:
                    {
                        DrawUtilityText();
                        DrawWaterworksImage();
                        break;
                    }
                case int _ when DeckObject.CardValue < 41:
                    {
                        DrawProperties();
                        break;
                    }
                case 41:
                    {
                        DrawChance();
                        break;
                    }
                case 42:
                    {
                        DrawGo();
                        break;
                    }
                default:
                    {
                        //has to ignore because no runtime error messages anyways.
                        break;
                    }
            }
        }
        private void DrawProperties()
        {
            if (DeckObject == null)
            {
                return;
            }
            string fillColor;
            string foreColor;
            string text1;
            string text2;
            switch (DeckObject.CardValue)
            {
                case 19:
                    {
                        text1 = "Medit Avenue";
                        fillColor = cc.Purple;
                        foreColor = cc.White;
                        text2 = "1 of 2";
                        break;
                    }
                case 20:
                    {
                        text1 = "Baltic Avenue";
                        fillColor = cc.Purple;
                        foreColor = cc.White;
                        text2 = "2 of 2";
                        break;
                    }
                case 21:
                    {
                        text1 = "Oriental Avenue";
                        fillColor = cc.Aqua;
                        foreColor = cc.Black;
                        text2 = "1 of 3";
                        break;
                    }
                case 22:
                    {
                        text1 = "Vermont Avenue";
                        fillColor = cc.Aqua;
                        foreColor = cc.Black;
                        text2 = "2 of 3";
                        break;
                    }
                case 23:
                    {
                        text1 = "Connect Avenue";
                        fillColor = cc.Aqua;
                        foreColor = cc.Black;
                        text2 = "3 of 3";
                        break;
                    }
                case 24:
                    {
                        text1 = "Charles Avenue";
                        fillColor = cc.Fuchsia;
                        foreColor = cc.Black;
                        text2 = "1 or 3";
                        break;
                    }
                case 25:
                    {
                        text1 = "States Avenue";
                        fillColor = cc.Fuchsia;
                        foreColor = cc.Black;
                        text2 = "2 of 3";
                        break;
                    }
                case 26:
                    {
                        text1 = "Virginia Avenue";
                        fillColor = cc.Fuchsia;
                        foreColor = cc.Black;
                        text2 = "3 of 3";
                        break;
                    }
                case 27:
                    {
                        text1 = "James Place";
                        fillColor = cc.DarkOrange;
                        foreColor = cc.Black;
                        text2 = "1 of 3";
                        break;
                    }
                case 28:
                    {
                        text1 = "Tenn Avenue";
                        fillColor = cc.DarkOrange;
                        foreColor = cc.Black;
                        text2 = "2 of 3";
                        break;
                    }
                case 29:
                    {
                        text1 = "York Avenue";
                        fillColor = cc.DarkOrange;
                        foreColor = cc.Black;
                        text2 = "3 of 3";
                        break;
                    }
                case 30:
                    {
                        text1 = "Kentucky Avenue";
                        fillColor = cc.Red;
                        foreColor = cc.White;
                        text2 = "1 of 3";
                        break;
                    }
                case 31:
                    {
                        text1 = "Indiana Avenue";
                        fillColor = cc.Red;
                        foreColor = cc.White;
                        text2 = "2 to 3";
                        break;
                    }
                case 32:
                    {
                        text1 = "Illinois Avenue";
                        fillColor = cc.Red;
                        foreColor = cc.White;
                        text2 = "3 of 3";
                        break;
                    }
                case 33:
                    {
                        text1 = "Atlantic Avenue";
                        fillColor = cc.Yellow;
                        foreColor = cc.Black;
                        text2 = "1 of 3";
                        break;
                    }
                case 34:
                    {
                        text1 = "Ventor Avenue";
                        fillColor = cc.Yellow;
                        foreColor = cc.Black;
                        text2 = "2 of 3";
                        break;
                    }
                case 35:
                    {
                        text1 = "Marvin Gardens";
                        fillColor = cc.Yellow;
                        foreColor = cc.Black;
                        text2 = "3 of 3";
                        break;
                    }
                case 36:
                    {
                        text1 = "Pacific Avenue";
                        fillColor = cc.Green;
                        foreColor = cc.White;
                        text2 = "1 of 3";
                        break;
                    }
                case 37:
                    {
                        text1 = "Carolina Avenue";
                        fillColor = cc.Green;
                        foreColor = cc.White;
                        text2 = "2 of 3";
                        break;
                    }
                case 38:
                    {
                        text1 = "Penn Avenue";
                        fillColor = cc.Green;
                        foreColor = cc.White;
                        text2 = "3 of 3";
                        break;
                    }
                case 39:
                    {
                        text1 = "Park Place";
                        fillColor = cc.DarkBlue;
                        foreColor = cc.White;
                        text2 = "1 of 2";
                        break;
                    }
                case 40:
                    {
                        text1 = "Board Walk";
                        fillColor = cc.DarkBlue;
                        foreColor = cc.White;
                        text2 = "2 of 2";
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            var firstRect = new RectangleF(3, 3, 45, 36);
            DrawRectangle(firstRect, fillColor);
            var thisList = text1.Split(" ").ToCustomBasicList();
            var fontSize = 9;
            if (thisList.Count != 2)
            {
                return;
            }
            int tops;
            tops = 1;
            foreach (var thisText in thisList)
            {
                var thisRect = new RectangleF(1, tops, 53, 18);
                DrawText(thisText, thisRect, foreColor, fontSize);
                tops += 18;
            }
            var secondRect = new RectangleF(0, 37, 55, 35);
            fontSize = 15;
            DrawText(text2, secondRect, cc.Black, fontSize);
        }
        private void DrawTokenText()
        {
            var thisRect = new RectangleF(0, 0, 55, 25);
            var fontSize = 15;
            DrawText("Token", thisRect, cc.Black, fontSize);
        }
        private void DrawChance()
        {
            var firstRect = new RectangleF(2, 0, 51, 26);
            var fontSize = 12;
            DrawText("Chance", firstRect, cc.Black, fontSize);
            var secondRect = new RectangleF(0, 10, 60, 70);
            fontSize = 45;
            DrawText("?", secondRect, cc.Red, fontSize, true);
        }
        private void DrawGo()
        {
            var firstRect = new RectangleF(0, 0, 55, 40);
            var fontSize = 30;
            DrawText("Go", firstRect, cc.Red, fontSize, true);
            var secondRect = new RectangleF(0, 35, 55, 20);
            fontSize = 12;
            var thirdRect = new RectangleF(0, 51, 55, 20);
            DrawText("Collect", secondRect, cc.Black, fontSize);
            DrawText("$200", thirdRect, cc.Black, fontSize);
        }
        private void DrawMRMonopolyText()
        {
            var fontSize = 9;
            var firstRect = new RectangleF(0, 5, 55, 15);
            var secondRect = new RectangleF(-2, 15, 55, 15);
            DrawText("Mr.", firstRect, cc.Black, fontSize);
            DrawText("Monopoly", secondRect, cc.Black, fontSize);
        }
        private void DrawHouseText()
        {
            if (DeckObject == null)
            {
                return;
            }
            var firstRect = new RectangleF(2, 2, 51, 18);
            var secondRect = new RectangleF(2, 15, 51, 18);
            string firstText = "";
            if (DeckObject.CardValue == 8)
            {
                firstText = "1st";
            }
            else if (DeckObject.CardValue == 9)
            {
                firstText = "2nd";
            }
            else if (DeckObject.CardValue == 10)
            {
                firstText = "3rd";
            }
            else if (DeckObject.CardValue == 11)
            {
                firstText = "4th";
            }
            var fontSize = 13;
            DrawText(firstText, firstRect, cc.Black, fontSize);
            DrawText("House", secondRect, cc.Black, fontSize);
        }
        private void DrawHotel()
        {
            var firstRect = new RectangleF(2, 2, 51, 28);
            var fontSize = 14;
            DrawText("Hotel", firstRect, cc.Black, fontSize);
            var bounds = new RectangleF(15, 30, 25, 35);
            DrawRectangle(bounds, cc.Red);
            int int_Row;
            int int_Col;
            RectangleF temps;
            for (int_Row = 0; int_Row <= 2; int_Row++)
            {
                for (int_Col = 0; int_Col <= 1; int_Col++)
                {
                    temps = new RectangleF(bounds.Location.X + (bounds.Width / 20) + ((bounds.Width / 5) * int_Col), bounds.Location.Y + (bounds.Height / 10) + ((bounds.Height / 5) * int_Row), bounds.Width / 6, bounds.Height / 6);
                    DrawRectangle(temps, cc.White);
                }
                for (int_Col = 1; int_Col <= 2; int_Col++)
                {
                    temps = new RectangleF(bounds.Location.X + (bounds.Width * 19 / 20) - (bounds.Width / 5 * int_Col), bounds.Location.Y + (bounds.Height / 10) + (bounds.Height / 5) * int_Row, bounds.Width / 6, bounds.Height / 6);
                    DrawRectangle(temps, cc.White);
                }
            }
            temps = new RectangleF(bounds.Location.X + (bounds.Width / 2) - (bounds.Width / 10), bounds.Location.Y + (bounds.Height * 4 / 5), bounds.Width / 5, bounds.Height / 5);
            DrawRectangle(temps, cc.White);
        }
        private void DrawRailRoadText()
        {
            if (DeckObject == null)
            {
                return;
            }
            var firstRect = new RectangleF(0, 2, 55, 15);
            var secondRect = new RectangleF(0, 15, 55, 15);
            var fontSize = 10;
            string color = cc.Black;
            string firstText = "";
            if (DeckObject.CardValue == 13)
            {
                firstText = "Reading";
            }

            else if (DeckObject.CardValue == 14)
            {
                firstText = "Penny";
            }
            else if (DeckObject.CardValue == 15)
            {
                firstText = "B & O";
            }
            else if (DeckObject.CardValue == 16)
            {
                firstText = "Shortline";
            }
            DrawText(firstText, firstRect, color, fontSize);
            DrawText("Railroad", secondRect, color, fontSize);
        }
        private void DrawUtilityText()
        {
            var thisRect = new RectangleF(0, 0, 55, 26);
            var fontSize = 15;
            DrawText("Utility", thisRect, cc.Black, fontSize);
        }
        private void DrawTokenImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            string imageName = "";
            if (DeckObject.CardValue == 1)
            {
                imageName = "Dog.png";
            }
            else if (DeckObject.CardValue == 2)
            {
                imageName = "Horse.png";
            }
            else if (DeckObject.CardValue == 3)
            {
                imageName = "Iron.png";
            }
            else if (DeckObject.CardValue == 4)
            {
                imageName = "Car.png";
            }
            else if (DeckObject.CardValue == 5)
            {
                imageName = "Ship.png";
            }
            else if (DeckObject.CardValue == 6)
            {
                imageName = "Hat.png";
            }
            var bounds = new RectangleF(6, 25, 40, 40);
            DrawPiece(imageName, bounds);
        }
        private void DrawMrMonopolyImage()
        {
            var thisRect = new RectangleF(13, 30, 25, 35);
            DrawPiece("MrMonopoly.png", thisRect);
        }
        private void DrawHouseImage()
        {
            var thisRect = new RectangleF(0, 35, 50, 30);
            DrawPiece("House.png", thisRect);
        }
        private void DrawRailroadImage()
        {
            var thisRect = new RectangleF(2, 30, 45, 30);
            DrawPiece("RailRoad.png", thisRect);
        }
        private void DrawElectricImage()
        {
            var thisRect = new RectangleF(7, 25, 40, 40);
            DrawPiece("Electric.png", thisRect);
        }
        private void DrawWaterworksImage()
        {
            var thisRect = new RectangleF(7, 25, 40, 40);
            DrawPiece("Waterworks.png", thisRect);
        }
        private void DrawPiece(string fileName, RectangleF bounds)
        {
            Image image = new Image();
            image.PopulateFullExternalImage(this, fileName);
            image.PopulateImagePositionings(bounds);
            MainGroup!.Children.Add(image);
        }
        private void DrawText(string content, RectangleF bounds, string color, float fontSize, bool hasBorders = false)
        {
            Text text = new Text();
            text.Content = content;
            text.CenterText(MainGroup!, bounds);
            text.Fill = color.ToWebColor();
            text.Font_Size = fontSize;
            text.Font_Weight = "bold";
            if (hasBorders)
            {
                text.PopulateStrokesToStyles();
            }
        }
        private void DrawRectangle(RectangleF bounds, string color)
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(bounds);
            rect.Fill = color.ToWebColor();
            MainGroup!.Children.Add(rect);
        }
    }
}