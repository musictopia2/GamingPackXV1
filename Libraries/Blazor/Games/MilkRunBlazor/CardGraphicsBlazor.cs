using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using MilkRunCP.Cards;
using MilkRunCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MilkRunBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<MilkRunCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            return DeckObject.CardCategory != EnumCardCategory.None && DeckObject.MilkCategory != EnumMilkType.None;
        }
        private void DrawText(string content, RectangleF bounds, string color, float fontSize)
        {
            Text text = new Text();
            text.Content = content;
            text.Fill = color.ToWebColor();
            text.CenterText(MainGroup!, bounds);
            text.Font_Size = fontSize;
            text.PopulateStrokesToStyles();
        }
        private void DrawRectangle(RectangleF bounds, string color)
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(bounds);
            rect.Fill = color.ToWebColor();
            MainGroup!.Children.Add(rect);
        }
        protected override void DrawBacks()
        {
            RectangleF firstRect;
            firstRect = new RectangleF(4, 4, 42, 28);
            var secondRect = new RectangleF(4, 35, 42, 28);
            DrawRectangle(firstRect, cc.DeepPink);
            DrawRectangle(secondRect, cc.Chocolate);
            CustomBasicList<RectangleF> thisList = new CustomBasicList<RectangleF>();
            float fontSize = DefaultSize.Height / 3.2f;
            string color = cc.Aqua;
            thisList.Add(firstRect);
            thisList.Add(secondRect);
            int x = 0;
            string thisText;
            foreach (var thisRect in thisList)
            {
                x += 1;
                switch (x)
                {
                    case 1:
                        {
                            thisText = "Milk";
                            break;
                        }
                    case 2:
                        {
                            thisText = "Run";
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                DrawText(thisText, thisRect, color, fontSize);
            }
        }
        private void DrawPiece()
        {
            if (DeckObject == null || MainGroup == null)
            {
                return;
            }
            var bounds = new RectangleF(2, 2, DefaultSize.Width - 6, DefaultSize.Height / 2.1f);
            string fileName;
            if (DeckObject.MilkCategory == EnumMilkType.Chocolate)
            {
                fileName = "chocolate.svg";
            }
            else if (DeckObject.MilkCategory == EnumMilkType.Strawberry)
            {
                fileName = "strawberry.svg";
            }
            else
            {
                return;
            }
            Image image = new Image();
            image.PopulateFullExternalImage(this, fileName);
            image.PopulateImagePositionings(bounds);
            MainGroup.Children.Add(image);
        }
        private void DrawOval(RectangleF bounds, string color)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.PopulateEllipse(bounds);
            ellipse.Fill = color.ToWebColor();
            MainGroup!.Children.Add(ellipse);
        }
        protected override void DrawImage()
        {
            if (DeckObject == null || MainGroup == null)
            {
                return;
            }
            DrawPiece();
            string thisText = "";
            var circleRect = new RectangleF(3, 33, 45, 30);
            var secondRect = new RectangleF(3, 35, 45, 30);
            if (DeckObject.CardCategory != EnumCardCategory.Joker)
            {
                if (DeckObject.CardCategory == EnumCardCategory.Go)
                {
                    thisText = "Go";
                }
                else if (DeckObject.CardCategory == EnumCardCategory.Stop)
                {
                    thisText = "Stop";
                }
                else
                {
                    thisText = DeckObject.Points.ToString();
                }
            }
            switch (DeckObject.CardCategory)
            {
                case EnumCardCategory.Joker:
                    var newRect = new RectangleF(13, 34, 29, 29);
                    if (DeckObject.MilkCategory == EnumMilkType.Chocolate)
                    {
                        MainGroup.DrawSmiley(newRect, cc.Chocolate, cc.Black, cc.Black, 2);
                    }
                    else if (DeckObject.MilkCategory == EnumMilkType.Strawberry)
                    {
                        MainGroup.DrawSmiley(newRect, cc.DeepPink, cc.Black, cc.Black, 2);
                    }
                    return;
                case EnumCardCategory.Go:
                    DrawOval(circleRect, cc.Lime);
                    break;
                case EnumCardCategory.Stop:
                    DrawOval(circleRect, cc.Red);
                    break;
                default:
                    break;
            }
            if (thisText == "")
            {
                return;
            }
            var fontSize = secondRect.Height * 1f;
            if (thisText == "Stop")
            {
                fontSize = secondRect.Height * .65f; //otherwise parts get cut off.
            }
            DrawText(thisText, secondRect, cc.White, fontSize);
        }
    }
}