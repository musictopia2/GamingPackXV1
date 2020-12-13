using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;

namespace SnakesAndLaddersBlazor
{
    public class PieceGraphicsBlazor : ComponentBase
    {
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }

        //[Parameter]
        //public string MainColor { get; set; } = cc.Transparent; //if not set, then nothing will show obviously.


        [Parameter]
        public int Index { get; set; }

        private string GetColor()
        {
            switch (Index)
            {
                case 1:
                    return cc.Blue;
                case 2:
                    return cc.DeepPink;
                case 3:
                    return cc.Orange;
                case 4:
                    return cc.ForestGreen;
                default:
                    return "";
            }
        }

        [Parameter]
        public int Number { get; set; }
        protected override void OnInitialized()
        {
            //this is where i set the values on the main graphics.
            MainGraphics!.OriginalSize = new SizeF(50, 50); //decided to use 300 by 300 this time.
            MainGraphics.BorderWidth = 1;
            base.OnInitialized();
        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Index == 0)
            {
                return;
            }
            ISvg svg = MainGraphics!.GetMainSvg(); //maybe has to be this way now.
            string color = GetColor();
            SvgRenderClass render = new SvgRenderClass();
            //MainGraphics!.CreateClick(svg); //has to do everytime.  otherwise, requires overriding.  i think this is fine.

            //this time, no clicking of unit necessary i think.
            Circle circle = new Circle();
            circle.PopulateCircle(0, 0, 50, color); //can experiment with the numbers for the transparency.
            //circle.Fill_Opacity = ".5";
            //maybe not even needed for transparency here.
            svg.Children.Add(circle);
            Text text = new Text();
            text.Fill = cc.White.ToWebColor();
            text.Font_Size = 40; //i think (?)
            text.PopulateStrokesToStyles(strokeWidth: 1);
            text.CenterText();
            if (Number == 0)
            {
                text.Content = Index.ToString();
            }
            else
            {
                text.Content = Number.ToString();
            }
            svg.Children.Add(text);
            //hopefully this simple (?)


            render.RenderSvgTree(svg, 0, builder);

            base.BuildRenderTree(builder);
            //no longer the razor syntax this time.



        }
    }
}
