using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using System.Reflection;
using XactikaCP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace XactikaBlazor
{
    public class StatsBoardBlazor : ComponentBase
    {
        ISvg? _mains;
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            _mains = new SVG();
            SvgRenderClass render = new SvgRenderClass();
            Rect rect = new Rect();
            rect.Width = "100%";
            rect.Height = "100%";
            rect.Fill = cc.White.ToWebColor();
            _mains.Children.Add(rect);
            DrawTopRow();
            int x;
            int y;
            int tops;
            tops = 43;
            int lefts;
            var diff1X = 35;
            var diff2X = 15;
            var diffY = 20;
            bool doPaint = true;
            var fontSize = 15;
            for (x = 4; x <= 12; x++)
            {
                lefts = 3;
                for (y = 1; y <= 5; y++)
                {
                    int currentX;
                    if (y == 1 || y == 2)
                    {
                        currentX = diff1X;
                    }
                    else
                    {
                        currentX = diff2X;
                    }
                    var bounds = new RectangleF(lefts, tops, currentX, diffY);
                    if (doPaint == true)
                    {
                        rect = new Rect();
                        rect.PopulateRectangle(bounds);
                        rect.Fill = cc.LightGray.ToWebColor();
                        _mains.Children.Add(rect);
                    }
                    DrawBorders(bounds);
                    var displayValue = GetTextValue(y, x);
                    if (displayValue > 0)
                    {
                        DrawText(displayValue.ToString(), bounds, fontSize);
                    }
                    lefts += currentX;
                }
                tops += diffY;
                doPaint = !doPaint;
            }
            render.RenderSvgTree(_mains, 0, builder);
            base.BuildRenderTree(builder);
        }
        private void DrawBorders(RectangleF bounds)
        {
            if (_mains == null)
            {
                return;
            }
            Rect rect = new Rect();
            rect.PopulateRectangle(bounds);
            rect.Fill = "none";
            rect.PopulateStrokesToStyles(strokeWidth: 2);
            _mains.Children.Add(rect);
        }
        private int GetTextValue(int column, int row)
        {
            if (column == 1)
            {
                return row;
            }
            if (column == 2)
            {
                if (row == 4 || row == 12)
                {
                    return 1;
                }
                if (row == 5 || row == 11)
                {
                    return 4;
                }
                if (row == 6 || row == 10)
                {
                    return 10;
                }
                if (row == 7 || row == 9)
                {
                    return 16;
                }
                if (row == 8)
                {
                    return 19;
                }
                return -1; //obvious there is a problem.
            }
            if (column == 3)
            {
                if (row == 4 || row == 10)
                {
                    return 1;
                }
                if (row == 5 || row == 9)
                {
                    return 3;
                }
                if (row == 6 || row == 8)
                {
                    return 6;
                }
                if (row == 7)
                {
                    return 7;
                }
                return 0;
            }
            if (column == 4)
            {
                if (row == 5 || row == 11)
                {
                    return 1;
                }
                if (row == 6 || row == 10)
                {
                    return 3;
                }
                if (row == 7 || row == 9)
                {
                    return 6;
                }
                if (row == 8)
                {
                    return 7;
                }
                return 0;
            }
            if (column == 5)
            {
                if (row == 6 || row == 12)
                {
                    return 1;
                }
                if (row == 7 || row == 11)
                {
                    return 3;
                }
                if (row == 8 || row == 10)
                {
                    return 6;
                }
                if (row == 9)
                {
                    return 7;
                }
                return 0;
            }
            return -2;
        }
        private void DrawText(string content, RectangleF bounds, float fontSize)
        {
            if (_mains == null)
            {
                return;
            }
            Text text = new Text();
            text.Content = content;
            text.CenterText(_mains, bounds);
            text.Fill = cc.Black.ToWebColor();
            text.Font_Size = fontSize;
            text.Font_Weight = "bold";
        }
        private void DrawTopRow()
        {
            if (_mains == null)
            {
                return;
            }
            var firstRect = new RectangleF(3, 3, 35, 40);
            var secondRect = new RectangleF(38, 3, 35, 40);
            var thirdRect = new RectangleF(73, 3, 15, 40);
            var fourthRect = new RectangleF(88, 3, 15, 40);
            var fifthRect = new RectangleF(103, 3, 15, 40);
            DrawBorders(firstRect);
            DrawBorders(secondRect);
            DrawBorders(thirdRect);
            DrawBorders(fourthRect);
            DrawBorders(fifthRect);
            Assembly assembly = Assembly.GetAssembly(GetType())!;
            var fontSize = firstRect.Height * 0.28f;
            DrawText("Value", firstRect, fontSize);
            DrawText("Cards", secondRect, fontSize);
            var cubeSize = new SizeF(12, 12);
            var pointList = ImageHelpers.GetPoints(EnumShapes.Cubes, 1, thirdRect.Location, true, cubeSize.Height);
            foreach (var thisPoint in pointList)
            {
                var tempRect = new RectangleF(thisPoint, cubeSize);
                ImageHelpers.DrawCube(_mains, assembly, tempRect);
            }
            pointList = ImageHelpers.GetPoints(EnumShapes.Cubes, 2, fourthRect.Location, true, cubeSize.Height);
            foreach (var thisPoint in pointList)
            {
                var tempRect = new RectangleF(thisPoint, cubeSize);
                ImageHelpers.DrawCube(_mains, assembly, tempRect);
            }
            pointList = ImageHelpers.GetPoints(EnumShapes.Cubes, 3, fifthRect.Location, true, cubeSize.Height);
            foreach (var thisPoint in pointList)
            {
                var tempRect = new RectangleF(thisPoint, cubeSize);
                ImageHelpers.DrawCube(_mains, assembly, tempRect);
            }
        }
    }
}