using BasicGameFrameworkLibrary.Dominos;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Dominos
{
    public class BasicDominoBlazor<D> : BaseDeckGraphics<D>
        where D : class, IDominoInfo, new()
    {
        protected override SizeF DefaultSize => new SizeF(95, 31); //i think.
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true; //allow when its unknown to support the boneyard.
            }
            if (DeckObject!.HighestDomino == 0 || DeckObject.CurrentFirst == -1 || DeckObject.CurrentSecond == -1)
            {
                return false;
            }
            return true;
        }
        protected override void DrawBacks() { }
        private CustomBasicList<string> _rowList = new CustomBasicList<string>();
        private CustomBasicList<string> _columnList = new CustomBasicList<string>();
        protected override void DrawImage()
        {
            Path path = new Path();
            path.PopulateStrokesToStyles(strokeWidth: 4);
            path.Fill = "none";
            path.D = "M47.5 0L47.5 31"; //now i can hard code because of translations, rotations, and scalings.
            MainGroup!.Children.Add(path);
            DrawDice(MainGroup, DeckObject!.CurrentFirst);
            G g = new G();
            g.Transform = "translate(47.5,0)";
            MainGroup.Children.Add(g);
            DrawDice(g, DeckObject.CurrentSecond);
        }
        protected override void OnInitialized()
        {
            _rowList = new CustomBasicList<string>() { "25%", "50%", "75%" };
            if (DeckObject!.HighestDomino <= 9)
            {
                _columnList = new CustomBasicList<string>() { "25%", "50%", "75%" };
            }
            else if (DeckObject.HighestDomino <= 12)
            {
                _columnList = new CustomBasicList<string>() { "20%", "40%", "60%", "80%" };
            }
            else if (DeckObject.HighestDomino <= 15)
            {
                _columnList = new CustomBasicList<string>() { "20%", "35%", "50%", "65%", "80%" };
            }
            base.OnInitialized();
        }
        private string GetDominoColor(int value)
        {
            string output = value switch
            {
                0 => cc.Transparent,
                1 => cc.Blue,
                2 => cc.DeepPink,
                3 => cc.Red,
                4 => cc.Yellow,
                5 => cc.DarkOrange,
                6 => cc.Lime,
                7 => cc.Aqua,
                8 => cc.DimGray,
                9 => cc.LightPink,
                10 => cc.Purple,
                11 => cc.Green,
                12 => cc.Navy,
                13 => cc.Gold,
                14 => cc.Teal,
                15 => cc.Silver,
                _ => throw new BasicBlankException("Only 0 to 15 is supported for the paints"),
            };
            return output.ColorUsed();
        }
        private void DrawDice(G g, int value)
        {
            if (value == 0)
            {
                return; //don't draw it because its 0.
            }
            string color = GetDominoColor(value);
            if (_rowList.Count == 0 || _columnList.Count == 0)
            {
                throw new BasicBlankException("Must populate the rows and columns before you can draw domino dice");
            }
            SVG svg = new SVG();
            svg.Width = "47.5";
            svg.Height = "31";
            g.Children.Add(svg); //try this now.
            if (value <= 9)
            {
                DrawUpToNine(svg, value, color);
                return;
            }
            DrawAfterNine(svg, value, color);
        }
        private void DrawUpToNine(ISvg svg, int value, string color)
        {
            float thisDec = (_columnList.Count + 1) / 2;
            int int_CenterColumn = (int)Math.Floor(thisDec);
            int_CenterColumn++;
            switch (value)
            {
                case 1:
                    AddCircle(svg, color, int_CenterColumn - 1, 2);
                    break;
                case 2:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    break;
                case 3:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn - 1, 2);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    break;
                case 4:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    AddCircle(svg, color, int_CenterColumn - 2, 3);
                    AddCircle(svg, color, int_CenterColumn, 1);
                    break;
                case 5:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    AddCircle(svg, color, int_CenterColumn - 2, 3);
                    AddCircle(svg, color, int_CenterColumn, 1);
                    AddCircle(svg, color, int_CenterColumn - 1, 2);
                    break;
                case 6:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    AddCircle(svg, color, int_CenterColumn - 2, 3);
                    AddCircle(svg, color, int_CenterColumn, 1);
                    AddCircle(svg, color, int_CenterColumn - 2, 2);
                    AddCircle(svg, color, int_CenterColumn, 2);
                    break;
                case 7:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    AddCircle(svg, color, int_CenterColumn - 2, 3);
                    AddCircle(svg, color, int_CenterColumn, 1);
                    AddCircle(svg, color, int_CenterColumn - 2, 2);
                    AddCircle(svg, color, int_CenterColumn, 2);
                    AddCircle(svg, color, int_CenterColumn - 1, 2);
                    break;
                case 8:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);
                    AddCircle(svg, color, int_CenterColumn, 3);
                    AddCircle(svg, color, int_CenterColumn - 2, 3);
                    AddCircle(svg, color, int_CenterColumn, 1);
                    AddCircle(svg, color, int_CenterColumn - 2, 2);
                    AddCircle(svg, color, int_CenterColumn, 2);
                    AddCircle(svg, color, int_CenterColumn - 1, 1);
                    AddCircle(svg, color, int_CenterColumn - 1, 3);
                    break;
                case 9:
                    AddCircle(svg, color, int_CenterColumn - 2, 1);

                    AddCircle(svg, color, int_CenterColumn, 3);
                    AddCircle(svg, color, int_CenterColumn - 2, 3);
                    AddCircle(svg, color, int_CenterColumn, 1);
                    AddCircle(svg, color, int_CenterColumn - 2, 2);
                    AddCircle(svg, color, int_CenterColumn, 2);
                    AddCircle(svg, color, int_CenterColumn - 1, 1);
                    AddCircle(svg, color, int_CenterColumn - 1, 2);
                    AddCircle(svg, color, int_CenterColumn - 1, 3);
                    break;
                default:
                    throw new BasicBlankException("Only up to 9");
            }
        }
        private void DrawAfterNine(ISvg svg, int value, string color)
        {
            DrawUpToNine(svg, 9, color);
            switch (value)
            {
                case 10:
                    AddCircle(svg, color, 4, 2);
                    break;
                case 11:
                    AddCircle(svg, color, 4, 1); //if 11 or 12
                    AddCircle(svg, color, 4, 3); //12 only
                    break;
                case 12:
                    DrawAtLeast12(svg, color);
                    break;
                case 13:
                    DrawAtLeast12(svg, color);
                    AddCircle(svg, color, 5, 2);
                    break;
                case 14:
                    DrawAtLeast12(svg, color);
                    AddCircle(svg, color, 5, 1);
                    AddCircle(svg, color, 5, 3);
                    break;
                case 15:
                    DrawAtLeast12(svg, color);
                    AddCircle(svg, color, 5, 2); //13 or 15
                    AddCircle(svg, color, 5, 3); //if 14 or 15
                    AddCircle(svg, color, 5, 1); //14 or 15 only
                    break;
                default:
                    throw new BasicBlankException("Only up to 15 are supported now");
            }
        }
        private void DrawAtLeast12(ISvg svg, string color)
        {
            AddCircle(svg, color, 4, 2); //10 value is 10 or 12
            AddCircle(svg, color, 4, 1); //if 11 or 12
            AddCircle(svg, color, 4, 3); //12 only
        }
        private void AddCircle(ISvg svg, string color, int column, int row)
        {
            Circle circle = new Circle();
            circle.Fill = color;
            circle.PopulateStrokesToStyles();
            circle.R = "2.5";
            circle.CX = _columnList[column - 1];
            circle.CY = _rowList[row - 1];
            svg.Children.Add(circle);
        }

    }
}