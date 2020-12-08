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
    internal record ListRecord(string Display, string Color, bool IsSelected, bool Enabled);
    public class ListViewPiece : ComponentBase
    {
        private ListRecord? _previousRecord;
        protected override void OnAfterRender(bool firstRender)
        {
            _previousRecord = GetRecord;
            base.OnAfterRender(firstRender);
        }
        private ListRecord GetRecord => new ListRecord(DataContext!.DisplayText, TextColor, MainGraphics!.IsSelected, MainGraphics.CustomCanDo.Invoke());
        protected override bool ShouldRender()
        {
            if (_previousRecord == null)
            {
                return true; //try this way just in case.
            }    
            return _previousRecord!.Equals(GetRecord) == false;
        }
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; } //still needed to get the start of the svg.  plus needs to have its start rectangle anyways.
        [Parameter]
        public ListPieceModel? DataContext { get; set; }
        [Parameter]
        public bool CanHighlight { get; set; } = true;
        [Parameter]
        public string TextColor { get; set; } = cc.Navy;
        protected virtual void SelectProcesses() { }
        protected virtual bool CanDrawText()
        {
            return true;
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (CanDrawText() == false)
            {
                return; //can't even continue because you can't draw.
            }
            SelectProcesses();
            ISvg svg = MainGraphics!.GetMainSvg(); //maybe has to be this way now.  this is like the draw processes.
            SvgRenderClass render = new SvgRenderClass();
            render.Allow0 = true; //try this.  because games like payday may need 0 to be allowed (?)
            Text text = new Text();
            text.CenterText();
            text.Fill = TextColor.ToWebColor();
            text.Content = DataContext!.DisplayText;
            svg.Children.Add(text);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
    }
}