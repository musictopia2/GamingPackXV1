using BasicGamingUIBlazorLibrary.BasicControls.ChoicePickers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Base
{
    public partial class BasePieceGraphics : GraphicsCommand
    {
        //this will set all the values that is the base.
        //this means a person would use this then the template will be graphics item

        [Parameter]
        public bool Animating { get; set; } = false; //maybe if animating, must render period now.  try that idea next.

        [Parameter]
        public bool ForceRender { get; set; }


        public string FillColor { get; set; } = cc.Aqua; //defaults to aqua but child can set to something else.
        [Parameter]
        public RenderFragment? ChildContent { get; set; } //only one is required here.
        [CascadingParameter]
        public ListChooserBlazor? ListPicker { get; set; }
        [CascadingParameter]
        public NumberChooserBlazor? NumberPicker { get; set; }
        [Parameter]
        public string TargetSize { get; set; } = ""; //this is for the target that is longest.
        [Parameter]
        public string TargetWidth { get; set; } = ""; //this focus on width alone.
        [Parameter]
        public string TargetHeight { get; set; } = ""; //this focus on height alone.
        [Parameter]
        public float LongestSize { get; set; } //this means ui can't update it.
        public bool NeedsHighlighting { get; set; } //no longer needs to be observable object.  this way it can't anyways.
        public bool HighlightTransparent { get; set; } //the child can set it when necessary.
        [Parameter]
        public PointF Location { get; set; } //iffy.  well see when we do animations.
        public SizeF OriginalSize { get; set; }
        public RectangleF GetRectangle => new RectangleF(0, 0, OriginalSize.Width, OriginalSize.Height); //try this way since this should already handle the locations.
        [Parameter]
        public bool IsSelected { get; set; }
        public bool UsedSelected = false; // the checker piece will not use that part.  if that changes, can do.
        public float BorderWidth { get; set; } = 4;
        public float Scale()
        {
            if (LongestSize == 0)
            {
                return 1;
            }
            if (OriginalSize.Width <= OriginalSize.Height)
            {
                return LongestSize / OriginalSize.Height;
            }
            return LongestSize / OriginalSize.Width;
        }
        private string GetHighlightColor()
        {
            if (NeedsHighlighting == false)
            {
                return cc.Transparent.ToWebColor(); //this means won't even do
            }
            if (IsSelected)
            {
                return cc.Lime.ToWebColor(); //will be lime
            }
            if (CustomCanDo.Invoke() == false)
            {
                return cc.LightGray.ToWebColor();
            }
            if (HighlightTransparent)
            {
                return cc.Transparent.ToWebColor();
            }
            return FillColor.ToWebColor();
        }

        //for now, can't do anything here to improve performance.
        //if i need improvements, then requires serious rethinking.
        //for example, on solitaire board game, when i select something, the color changes.
        //good news is on solitaire board game, animation is not bad.


        //private bool _wasFirst;
        //protected override void OnAfterRender(bool firstRender)
        //{
        //    _wasFirst = firstRender;
        //    if (firstRender == true)
        //    {
        //        StateHasChanged();
        //    }
        //    base.OnAfterRender(firstRender);
        //}
        //protected override bool ShouldRender()
        //{
        //    return _wasFirst;
        //}


        private void PopulateCustomViewBox(ISvg svg)
        {
            var value = (BorderWidth / 2) * -1;
            svg.ViewBox = $"{value} {value} {OriginalSize.Width + BorderWidth} {OriginalSize.Height + BorderWidth}";
        }

        public ISvg GetMainSvg(bool? controlhighlight = null)
        {
            ISvg output = new SVG();
            if (TargetSize != "")
            {
                if (controlhighlight == null || controlhighlight == true)
                {
                    NeedsHighlighting = true; //otherwise, the control can set either way.
                }
                if (OriginalSize.Width >= OriginalSize.Height)
                {
                    output.Height = TargetSize;
                }
                else
                {
                    output.Width = TargetSize;
                }
                PopulateCustomViewBox(output);
                output.Style = "margin: .2vh;";
            }
            else if (TargetHeight != "")
            {
                output.Height = TargetHeight;
                output.Style = "margin: .2vh;";
                PopulateCustomViewBox(output);
            }
            else if (TargetWidth != "")
            {
                output.Width = TargetWidth;
                output.Style = "margin: .2vw;";
                PopulateCustomViewBox(output);
            }
            else if (ListPicker != null) //could do interfaces.  if so, then the interface can determine the text size.
            {
                NeedsHighlighting = ListPicker.CanHighlight; //hopefully this simple this time.
                output.Height = ListPicker.TextHeight;
                output.Style = "margin-right: .2vw; margin-bottom: .2vw; margin-left: .2vw;";

                output.ViewBox = $"0 0 {ListPicker.TextWidth} 18";
            }
            else if (NumberPicker != null)
            {
                NeedsHighlighting = NumberPicker.CanHighlight;
                output.Height = NumberPicker.TextHeight;
                output.Style = "margin-right: .2vw; margin-bottom: .2vw; margin-left: .2vw;";
                output.ViewBox = $"0 0 {NumberPicker!.TextWidth} 40"; //can rethink if necessary.
            }
            else
            {
                output.X = Location.X.ToString();
                output.Y = Location.Y.ToString();
                var value = Scale() * OriginalSize.Width;
                output.Width = value.ToString();
                value = Scale() * OriginalSize.Height;
                output.Height = value.ToString();
                value = (BorderWidth / 2) * -1;
                output.ViewBox = $"{value} {value} {OriginalSize.Width + BorderWidth} {OriginalSize.Height + BorderWidth}";
            }
            Rect rect = new Rect();
            rect.Width = "100%";
            rect.Height = "100%";
            rect.Fill = GetHighlightColor();
            output.Children.Add(rect);
            CreateClick(output); //now it can do automatically.
            return output;
        }
    }
}