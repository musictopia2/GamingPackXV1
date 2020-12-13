using BasicGameFrameworkLibrary.ColorCards;
using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using UnoCP.Cards;
using UnoCP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace UnoBlazor
{
    public class CardGraphicsBlazor : BaseColorCardsImageBlazor<UnoCardInformation>
    {
        protected override string BackColor => cc.Red;
        protected override string BackFontColor => cc.White;
        protected override string BackText => "Uno";
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            if (DeckObject!.WhichType == EnumCardTypeList.None)
            {
                return false;
            }
            return true;
        }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            if (DeckObject.WhichType == EnumCardTypeList.Draw2)
            {
                DrawValueCard(DefaultRectangle, "+2");
                return;
            }
            if (DeckObject.WhichType == EnumCardTypeList.Skip)
            {
                DrawValueCard(DefaultRectangle, "S");
                return;
            }
            if (DeckObject.WhichType == EnumCardTypeList.Reverse)
            {
                DrawValueCard(DefaultRectangle, "R");
                return;
            }
            if (DeckObject.WhichType == EnumCardTypeList.Regular)
            {
                DrawValueCard(DefaultRectangle, DeckObject.Number.ToString());
                return;
            }
            if (DeckObject.WhichType == EnumCardTypeList.Wild)
            {
                string text;
                if (DeckObject.Draw == 0)
                {
                    text = "W";
                }
                else
                {
                    text = "+4";
                }
                if (DeckObject.Color == EnumColorTypes.ZOther)
                {
                    DrawFourRectangles();
                    if (DeckObject.Drew || DeckObject.IsSelected)
                    {
                        DrawHighlighters(); //try this here.
                    }
                }
                DrawValueCard(DefaultRectangle, text);
            }
        }
        private void DrawFourRectangles()
        {
            RectangleF rect_Card = DefaultRectangle;
            float widths;
            float heights;
            widths = rect_Card.Width / 2;
            heights = rect_Card.Height / 2;
            RectangleF newRect;
            newRect = new RectangleF(rect_Card.Left + 2, rect_Card.Top + 2, widths - 4, heights - 4);
            DrawSingleRectangle(newRect, cc.Red);
            newRect = new RectangleF(rect_Card.Left + widths - 2, rect_Card.Top + 2, widths - 4, heights - 4);
            DrawSingleRectangle(newRect, cc.Yellow);
            newRect = new RectangleF(rect_Card.Left + widths - 2, rect_Card.Top + heights - 2, widths - 4, heights - 4);
            DrawSingleRectangle(newRect, cc.Blue);
            newRect = new RectangleF(rect_Card.Left + 2, rect_Card.Top + heights - 2, widths - 4, heights - 4);
            DrawSingleRectangle(newRect, cc.Green);
        }
        private void DrawSingleRectangle(RectangleF newRect, string color)
        {
            Rect r = new Rect();
            r.PopulateRectangle(newRect);
            r.Fill = color.ToWebColor();
            MainGroup!.Children.Add(r);
        }
    }
}