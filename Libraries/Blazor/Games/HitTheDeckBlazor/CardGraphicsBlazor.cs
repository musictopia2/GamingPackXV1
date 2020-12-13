using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using HitTheDeckCP.Cards;
using HitTheDeckCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace HitTheDeckBlazor
{
    public class CardGraphicsBlazor : BaseDarkCardsBlazor<HitTheDeckCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72); //this is default but can change to anything you want.
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            if (DeckObject!.CardType == EnumTypeList.Regular && DeckObject!.Number == 0)
            {
                return false;
            }
            return DeckObject!.CardType != EnumTypeList.None;
        }
        protected override bool IsLightColored => DeckObject!.Color == cc.Yellow;
        protected override void DrawBacks()
        {
            RectangleF rect_Card = new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
            CustomBasicList<RectangleF> list = new CustomBasicList<RectangleF>();
            var fontSize = rect_Card.Height / 4f;
            var thisRect = new RectangleF(0, 0, rect_Card.Width, rect_Card.Height / 3);
            list.Add(thisRect);
            thisRect = new RectangleF(0, rect_Card.Height / 3.1f, rect_Card.Width, rect_Card.Height / 3);
            list.Add(thisRect);
            thisRect = new RectangleF(0, rect_Card.Height / 3 * 1.8f, rect_Card.Width, rect_Card.Height / 3);
            list.Add(thisRect);
            int x = 0;
            string content = "";
            foreach (var rect in list)
            {
                x += 1;
                switch (x)
                {
                    case 1:
                        content = "Hit";
                        break;
                    case 2:
                        content = "The";
                        break;
                    case 3:
                        content = "Deck";
                        break;
                    default:
                        break;
                }
                Text text = new Text();
                text.Content = content;
                text.Font_Size = fontSize;
                text.CenterText(MainGroup!, rect);
                text.PopulateStrokesToStyles(strokeWidth: .5f);
                text.Fill = cc.Yellow.ToWebColor();
            }
        }
        protected override void BeforeFilling()
        {
            FillColor = GetFillColor();
        }
        private string GetFillColor()
        {
            if (DeckObject!.IsUnknown)
            {
                return cc.Aqua; //aqua is fine anyways.
            }
            if (DeckObject.Color == cc.Transparent)
            {
                return cc.White;
            }
            return DeckObject.Color;
        }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            if (DeckObject.Number > 5)
            {
                return;
            }
            if (DeckObject.CardType != EnumTypeList.Regular && DeckObject.CardType != EnumTypeList.Number && DeckObject.Number != 0)
            {
                return;
            }
            RectangleF rect_Card = new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
            RectangleF tempCard;
            bool drew4;
            if (DeckObject.CardType != EnumTypeList.Draw4 && DeckObject.CardType != EnumTypeList.Color && DeckObject.CardType != EnumTypeList.Number)
            {
                drew4 = false;
            }
            else
            {
                drew4 = true;
                tempCard = new RectangleF(2, 2, rect_Card.Width - 4, rect_Card.Height - 4);
                DrawFourRectangles(tempCard);
            }
            if (DeckObject.CardType == EnumTypeList.Cut)
            {
                DrawCut(rect_Card);
            }
            else if (DeckObject.CardType == EnumTypeList.Flip)
            {
                DrawFlip(rect_Card);
            }
            else if (DeckObject.CardType == EnumTypeList.Number || DeckObject.CardType == EnumTypeList.Regular)
            {
                tempCard = new RectangleF(DefaultSize.Width / 15, DefaultSize.Height / 10, DefaultSize.Width / 4 * 3, DefaultSize.Height / 4 * 3);
                tempCard.X += 2;
                tempCard.Y += 2;
                DrawBorderText(DeckObject.Number.ToString(), tempCard);
            }
            else if (DeckObject.CardType == EnumTypeList.Draw4)
            {
                rect_Card.X -= 2;
                DrawBorderText("+4?", rect_Card);
            }
            if (drew4)
            {
                DrawBorders();
            }
        }
        private void DrawFourRectangles(RectangleF rect_Card)
        {
            int widths;
            int heights;
            widths = (int)rect_Card.Width / 2;
            heights = (int)rect_Card.Height / 2;
            RectangleF newRect;
            newRect = new RectangleF(rect_Card.Left, rect_Card.Top, widths, heights);
            DrawSimpleRectangle(newRect, cc.Red);
            newRect = new RectangleF(rect_Card.Left + widths, rect_Card.Top, widths, heights);
            DrawSimpleRectangle(newRect, cc.Yellow);
            newRect = new RectangleF(rect_Card.Left + widths, rect_Card.Top + heights, widths, heights - 3);
            DrawSimpleRectangle(newRect, cc.Blue);
            newRect = new RectangleF(rect_Card.Left, rect_Card.Top + heights, widths, heights - 3);
            DrawSimpleRectangle(newRect, cc.Green);
            if ((DeckObject!.CardType == EnumTypeList.Color) | (DeckObject!.CardType == EnumTypeList.Draw4))
            {
                var newLeft = rect_Card.Left + (widths / 2);
                var newTop = rect_Card.Top + (heights / 2);
                newRect = new RectangleF(newLeft, newTop, widths, heights);
                DrawSimpleRectangle(newRect, DeckObject.Color, true); //hopefully this simple.
            }
        }
        private void DrawSimpleRectangle(RectangleF toUse, string color, bool hasBorders = false)
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(toUse);
            rect.Fill = color.ToWebColor();
            if (hasBorders)
            {
                rect.PopulateStrokesToStyles(strokeWidth: 2);
            }
            MainGroup!.Children.Add(rect);
        }
        private void DrawCut(RectangleF rect_Card)
        {
            Image image = new Image();
            image.PopulateFullExternalImage(this, "cut.png");
            image.PopulateImagePositionings(0, 0, rect_Card.Width, rect_Card.Height);
            MainGroup!.Children.Add(image);
        }
        private void DrawFlip(RectangleF rect_Card)
        {
            Image image = new Image();
            image.PopulateFullExternalImage(this, "flip.png");
            image.PopulateImagePositionings(rect_Card);
            MainGroup!.Children.Add(image);
        }
        private void DrawBorderText(string value, RectangleF rect_Card)
        {
            float fontSize;
            if (DeckObject!.CardType != EnumTypeList.Draw4)
            {
                fontSize = rect_Card.Height / .9f;
            }
            else
            {
                fontSize = rect_Card.Height / 2.7f;
            }
            Text text = new Text();
            text.Font_Size = fontSize;
            text.Content = value;
            text.CenterText(MainGroup!, rect_Card);
            text.PopulateStrokesToStyles();
            text.Fill = cc.White.ToWebColor();
        }
        private void DrawBorders()
        {
            Rect rect = StartRectangle();
            rect.PopulateStrokesToStyles(strokeWidth: (int)BorderWidth);
            MainGroup!.Children.Add(rect);
            if (DeckObject!.Drew || DeckObject.IsSelected)
            {
                DrawHighlighters();
            }
        }
    }
}