using BowlingDiceGameCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BowlingDiceGameBlazor
{
    public class BowlingSingleDiceBlazor : ComponentBase
    {

        [Parameter]
        public SingleDiceInfo? Dice { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Dice == null)
            {
                return;
            }
            SvgRenderClass render = new SvgRenderClass();
            ISvg svg = new SVG();
            Rect rect = new Rect();
            rect.Width = "50";
            rect.Height = "50";
            rect.Fill = cc.White.ToWebColor();
            svg.Children.Add(rect);
            if (Dice.DidHit == false)
            {
                Image image = new Image();
                image.Width = "50";
                image.Height = "50";
                image.PopulateFullExternalImage(this, "bowlingdice.png");
                svg.Children.Add(image);
            }
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}