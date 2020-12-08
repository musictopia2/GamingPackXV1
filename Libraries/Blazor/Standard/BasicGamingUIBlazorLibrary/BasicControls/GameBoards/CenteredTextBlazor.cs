using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    internal record CenteredTextRecord(string Text, string BorderColor, bool Bold);
    public class CenteredTextBlazor : ComponentBase
    {
        private CenteredTextRecord? _previous;
        protected override void OnAfterRender(bool firstRender)
        {
            _previous = GetRecord;
            base.OnAfterRender(firstRender);
        }
        
        private CenteredTextRecord GetRecord => new CenteredTextRecord(Text, BorderColor, Bold);

        [Parameter]
        public string Text { get; set; } = "";

        [Parameter]
        public float BorderWidth { get; set; } //have the option open on whether to have bordered text.  we may or may not do bordered text.
        [Parameter]
        public string BorderColor { get; set; } = cc.Transparent;
        [Parameter]
        public string TextColor { get; set; } = cc.Black;

        [Parameter]
        public double FontSize { get; set; } //must set font size.  don't do anything at defaut.
        [Parameter]
        public bool Bold { get; set; } //iffy.


        protected override bool ShouldRender()
        {
            return _previous!.Equals(GetRecord) == false;
        }

        [Parameter]
        public string FontFamily { get; set; } = "tahoma";
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            SvgRenderClass render = new SvgRenderClass();
            ISvg svg = new SVG();
            render.Allow0 = true;
            Text text = new Text(); //something else will handle clicking if necessary.
            text.CenterText();
            if (BorderWidth > 0 && BorderColor != cc.Transparent)
            {
                text.PopulateStrokesToStyles(BorderColor.ToWebColor(), BorderWidth, FontFamily);
            }
            else
            {
                text.Style = $"font-family: {FontFamily};";
            }
            text.Fill = TextColor.ToWebColor();
            text.Font_Size = FontSize;
            if (Bold)
            {
                text.Font_Weight = "bold";
            }
            text.Content = Text;
            svg.Children.Add(text);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}