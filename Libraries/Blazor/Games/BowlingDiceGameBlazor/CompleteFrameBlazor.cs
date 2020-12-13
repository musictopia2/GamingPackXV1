using BowlingDiceGameCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BowlingDiceGameBlazor
{
    public class CompleteFrameBlazor : ComponentBase
    {
        [Parameter]
        public FrameInfoCP? Frame { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Frame == null || Frame.SectionList.Count != 3)
            {
                return;
            }
            SvgRenderClass render = new SvgRenderClass();
            render.Allow0 = true;
            ISvg svg = new SVG();
            svg.Width = "150";
            svg.Height = "100";
            int borderSize = 4;
            float x = 0;
            Rect rect;
            foreach (var section in Frame.SectionList)
            {
                rect = new Rect();
                rect.Width = "50";
                rect.Height = "50";
                rect.X = x.ToString();
                rect.PopulateStrokesToStyles(cc.White.ToWebColor(), borderSize);
                svg.Children.Add(rect);
                RectangleF r = new RectangleF(x, 0, 50, 50);
                svg.DrawCenteredText(r, 20, section.Value.Score, cc.White);
                x += 50;
            }
            RectangleF f = new RectangleF(0, 50, 150, 50);
            rect = new Rect();
            rect.Width = "150";
            rect.Height = "50";
            rect.X = f.X.ToString();
            rect.Y = f.Y.ToString();
            rect.PopulateStrokesToStyles(cc.White.ToWebColor(), borderSize);
            svg.Children.Add(rect);
            string score;
            if (Frame.Score == -1)
            {
                score = "";
            }
            else
            {
                score = Frame.Score.ToString();
            }
            svg.DrawCenteredText(f, 20, score, cc.White);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}