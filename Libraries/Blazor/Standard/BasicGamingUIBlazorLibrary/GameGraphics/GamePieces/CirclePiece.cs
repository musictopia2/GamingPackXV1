using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.GamePieces
{
    internal record CircleRecord(string Color, bool NeedsWhiteBorders);
    public class CirclePiece : ComponentBase
    {
        protected override void OnAfterRender(bool firstRender)
        {
            _previousRecord = new CircleRecord(MainColor, NeedsWhiteBorders);
            base.OnAfterRender(firstRender);
        }
        protected override bool ShouldRender()
        {
            if (MainGraphics!.Animating)
            {
                return true; //because you are doing animations.
            }
            CircleRecord current = new CircleRecord(MainColor, NeedsWhiteBorders);
            return !current.Equals(_previousRecord);
        }


        private CircleRecord? _previousRecord;

        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }
        [Parameter]
        public string MainColor { get; set; } = cc.Transparent; //if not set, then nothing will show obviously.
        [Parameter]
        public bool NeedsWhiteBorders { get; set; }
        protected override void OnInitialized()
        {
            MainGraphics!.OriginalSize = new SizeF(100, 100); //easiest just to use 100 by 100.  of course, its flexible anyways.
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            if (NeedsWhiteBorders)
            {
                MainGraphics!.BorderWidth = 8;
            }
            else
            {
                MainGraphics!.BorderWidth = 4;
            }
            base.OnParametersSet();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ISvg svg = MainGraphics!.GetMainSvg(false);
            SvgRenderClass render = new SvgRenderClass();
            Circle circle;
            if (NeedsWhiteBorders && MainColor != cc.Transparent)
            {
                circle = new Circle();
                circle.CX = "50";
                circle.CY = "50";
                circle.R = "50";
                circle.PopulateStrokesToStyles(cc.Black.ToWebColor(), (int)MainGraphics!.BorderWidth);
                circle.Fill = cc.White.ToWebColor();
                svg.Children.Add(circle);
                circle = new Circle();

                circle.CX = "50";
                circle.CY = "50";
                circle.R = "33";
                circle.Fill = MainColor.ToWebColor();
                circle.PopulateStrokesToStyles(cc.Black.ToWebColor(), 4);
                svg.Children.Add(circle);
            }
            else
            {
                circle = new Circle();
                circle.CX = "50";
                circle.CY = "50";
                circle.R = "50";
                circle.PopulateStrokesToStyles(cc.Black.ToWebColor(), (int)MainGraphics!.BorderWidth);
                circle.Fill = MainColor.ToWebColor();
                svg.Children.Add(circle);
            }
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}