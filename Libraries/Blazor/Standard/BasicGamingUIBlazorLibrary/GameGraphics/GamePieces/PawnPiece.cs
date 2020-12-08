using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.GamePieces
{


    public class PawnPiece : ComponentBase
    {
        private ColorRecord? _previousRecord;
        private ColorRecord GetRecord => new ColorRecord(MainColor, MainGraphics!.IsSelected, MainGraphics.CustomCanDo.Invoke());
        //private string _previousColor = "";
        protected override void OnAfterRender(bool firstRender)
        {
            _previousRecord = GetRecord;
            base.OnAfterRender(firstRender);
        }
        protected override bool ShouldRender()
        {
            if (MainGraphics!.Animating || MainGraphics.ForceRender)
            {
                return true; //because you are doing animations.
            }
            return _previousRecord!.Equals(GetRecord) == false; //hopefully this simple now (?)
        }

        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }

        [Parameter]
        public string MainColor { get; set; } = cc.Transparent; //if not set, then nothing will show obviously.

        protected override void OnInitialized()
        {
            //this is where i set the values on the main graphics.
            MainGraphics!.OriginalSize = new SizeF(100, 100); //decided to use 150 by 150 this time.
            MainGraphics.BorderWidth = 1;
            MainGraphics.HighlightTransparent = true; //i think.
            base.OnInitialized();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            ISvg svg = MainGraphics!.GetMainSvg(false); //try to set this to false (?)
            svg.DrawPawnPiece(MainColor);
            SvgRenderClass render = new SvgRenderClass();
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}