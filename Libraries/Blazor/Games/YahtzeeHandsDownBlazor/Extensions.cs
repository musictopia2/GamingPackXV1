using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
namespace YahtzeeHandsDownBlazor
{
    public static class Extensions
    {
        public static void DrawNormalRectangle(this IParentGraphic graphic, RectangleF rectangle, string color, float borderSize = 0)
        {
            Rect output = new Rect();
            graphic.Children.Add(output);
            output.PopulateRectangle(rectangle);
            output.Fill = color.ToWebColor();
            if (borderSize > 0)
            {
                output.PopulateStrokesToStyles(strokeWidth: borderSize);
            }
        }
        public static void DrawRoundedRectangle(this IParentGraphic graphic, RectangleF rectangle, string color, float radius, float borderSize)
        {
            Rect output = new Rect();
            graphic.Children.Add(output);
            output.PopulateRectangle(rectangle);
            output.Fill = color.ToWebColor();
            output.RX = radius.ToString();
            output.RY = radius.ToString();
            output.PopulateStrokesToStyles(strokeWidth: borderSize);
        }
        public static void DrawCustomText(this IParentGraphic graphic, RectangleF rectangle, string content, float fontSize, string color, bool bold = false)
        {
            Text text = new Text();
            text.CenterText(graphic, rectangle);
            text.Fill = color.ToWebColor();
            text.Font_Size = fontSize;
            text.Content = content;
            if (bold)
            {
                text.Font_Weight = "bold";
            }
        }
    }
}