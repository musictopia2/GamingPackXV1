using BasicGameFrameworkLibrary.GamePieceModels;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.GamePieces
{
    internal record NumberRecord(string Display, string TextColor, bool Enabled, bool IsSelected );
    public class NumberPiece : ComponentBase
    {
        private NumberRecord? _previousRecord;
        protected override void OnAfterRender(bool firstRender)
        {
            _previousRecord = GetRecord;
            base.OnAfterRender(firstRender);
        }
        private NumberRecord GetRecord => new NumberRecord(GetValueToPrint(), TextColor, MainGraphics!.CustomCanDo.Invoke(), MainGraphics.IsSelected);
        protected override bool ShouldRender()
        {
            var current = GetRecord;
            return current.Equals(_previousRecord) == false;
        }
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; } //still needed to get the start of the svg.  plus needs to have its start rectangle anyways.
        [Parameter]
        public NumberModel? DataContext { get; set; }
        protected virtual bool CanDrawNumber()
        {
            return true;
        }
        protected virtual void SelectProcesses() { }
        [Parameter]
        public bool CanHighlight { get; set; } = true;
        [Parameter]
        public string TextColor { get; set; } = cc.Navy;
        protected virtual string GetValueToPrint() // so the overrided version can display something else.
        {
            if (DataContext == null)
            {
                return ""; //i think this as well.
            }
            if (DataContext!.NumberValue < 0)
            {
                return "";
            }
            return DataContext!.NumberValue.ToString();
        }
        protected virtual void OriginalSizeProcesses() { }
        protected override void OnInitialized()
        {
            MainGraphics!.NeedsHighlighting = CanHighlight; //i think this was needed too.
            OriginalSizeProcesses();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (CanDrawNumber() == false)
            {
                return; //can't even continue because you can't draw.
            }
            string value = GetValueToPrint();
            if (value == "")
            {
                return;
            }
            SelectProcesses();
            ISvg svg = MainGraphics!.GetMainSvg(); //maybe has to be this way now.  this is like the draw processes.
            SvgRenderClass render = new SvgRenderClass();
            render.Allow0 = true; //i think allow here. as well.
            Text text = new Text();
            text.CenterText();
            if (value.Length == 3)
            {
                text.Font_Size = 20;
                text.PopulateStrokesToStyles();
            }
            else if (value.Length == 2)
            {
                text.Font_Size = 30;
                text.PopulateStrokesToStyles(strokeWidth: 2);
            }
            else
            {
                text.Font_Size = 40;
                text.PopulateStrokesToStyles(strokeWidth: 2);
            }
            text.Fill = TextColor.ToWebColor();
            text.Content = value;
            svg.Children.Add(text);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}