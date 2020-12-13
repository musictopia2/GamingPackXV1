using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace LifeBoardGameBlazor
{
    public class CarPieceBlazor : ComponentBase
    {
        private struct PegInfo
        {
            public double CX { get; set; }
            public double CY { get; set; }
            public double RX { get; set; }
            public double RY { get; set; }
        }
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }
        [Parameter]
        public string MainColor { get; set; } = cc.Transparent; //has to be this way so its compatible with the processes for the color pickers
        [Parameter]
        public LifeBoardGamePlayerItem? Player { get; set; } //if player is used, then no need for colorused.
        protected override void OnInitialized()
        {
            MainGraphics!.OriginalSize = new SizeF(136, 248);
            MainGraphics.BorderWidth = 1;
            MainGraphics.HighlightTransparent = true;
            base.OnInitialized();
        }
        private string RealColor()
        {
            if (Player == null)
            {
                return MainColor.ToWebColor();
            }
            return Player.Color.ToColor().ToWebColor();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ISvg svg = MainGraphics!.GetMainSvg(false);
            SvgRenderClass render = new SvgRenderClass();
            DrawBasicCar(svg);
            DrawPegs(svg);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
        private void DrawBasicCar(ISvg svg)
        {
            Rect rect = new Rect();
            rect.Fill = cc.Black.ToWebColor();
            rect.PopulateRectangle(0, 24.799999f, 136, 49.599995f);
            svg.Children.Add(rect);
            rect = new Rect();
            rect.Fill = cc.Black.ToWebColor();
            rect.PopulateRectangle(0, 173.60001f, 136, 49.600006f);
            svg.Children.Add(rect);
            PopulatePath(svg, "M125 239.733L11 239.733L20 8.26665L116 8.26666L125 239.733Z", true);
            PopulatePath(svg, "M117.111 231.467L68 245.52L18.8889 231.467L117.111 231.467Z");
            PopulatePath(svg, "M115.222 82.6667L68 74.4L20.7778 82.6666L20.7778 82.6666L30.2222 62L68 53.7333L105.778 62L115.222 82.6667Z");
            PopulatePath(svg, "M110.5 57.8667L68 49.6L25.5 57.8666L25.5 57.8666L34 8.26665L68 0L102 8.26666L110.5 57.8667Z");
        }
        private void PopulatePath(ISvg svg, string data, bool needsStroke = false)
        {
            Path path = new Path();
            path.Fill = RealColor();
            if (needsStroke)
            {
                path.PopulateStrokesToStyles();
            }
            path.D = data;
            svg.Children.Add(path);
        }
        private void DrawPegs(ISvg svg)
        {
            CustomBasicList<PegInfo> pegs = new CustomBasicList<PegInfo>()
            {
                new PegInfo()
                {
                    CX = 42.311111,
                    CY = 114.3111,
                    RX = 15.111111,
                    RY = 15.111107
                },

                new PegInfo()
                {
                    CX = 93.688889,
                    CY = 114.3111,
                    RX = 15.111111,
                    RY = 15.111107
                },

                new PegInfo()
                {
                    CX = 42.311111,
                    CY = 152.0889,
                    RX = 15.111111,
                    RY = 15.111115
                },

                new PegInfo()
                {
                    CX = 93.688889,
                    CY = 152.0889,
                    RX = 15.111111,
                    RY = 15.111115
                },

                new PegInfo()
                {
                    CX = 42.311111,
                    CY = 189.86667,
                    RX = 15.111111,
                    RY = 15.111115
                },

                new PegInfo()
                {
                    CX = 93.688889,
                    CY = 189.86667,
                    RX = 15.111111,
                    RY = 15.111115
                },

            };
            FigureOutGenders();
            foreach (var peg in pegs)
            {
                DrawSinglePeg(svg, peg, pegs.IndexOf(peg));
            }
        }
        private void FigureOutGenders()
        {
            if (Player == null || Player.Gender == EnumGender.None)
            {
                return;
            }
            _genderList.Clear();
            _genderList.Add(Player.Gender);
            if (Player.Married)
            {
                if (Player.Gender == EnumGender.Boy)
                {
                    _genderList.Add(EnumGender.Girl);
                }
                else
                {
                    _genderList.Add(EnumGender.Boy);
                }
            }
            Player.ChildrenList.ForEach(x =>
            {
                _genderList.Add(x);
            });
        }
        private readonly CustomBasicList<EnumGender> _genderList = new CustomBasicList<EnumGender>();
        private void DrawSinglePeg(ISvg svg, PegInfo peg, int index)
        {
            DrawPeg(svg, peg, cc.Black.ToWebColor());
            if (Player == null)
            {
                return;
            }
            if (index < _genderList.Count)
            {
                string color = _genderList[index].ToColor().ToWebColor();
                DrawPeg(svg, peg, color, true);
            }
        }
        private void DrawPeg(ISvg svg, PegInfo peg, string color, bool borders = false)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.CX = peg.CX.ToString();
            ellipse.CY = peg.CY.ToString();
            ellipse.RX = peg.RX.ToString();
            ellipse.RY = peg.RY.ToString();
            ellipse.Fill = color;
            if (borders)
            {
                ellipse.PopulateStrokesToStyles();
            }
            svg.Children.Add(ellipse);
        }
    }
}