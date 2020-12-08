using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    public partial class RawGameBoard : IDisposable, IHandle<RepaintEventModel>
    {
        private IEventAggregator? _aggregator;
        protected override void OnInitialized()
        {
            if (UseBuiltInAnimations == true)
            {
                _aggregator = cons!.Resolve<IEventAggregator>();
                _aggregator.Subscribe(this, EnumRepaintCategories.Main.ToString());
            }
            base.OnInitialized();
        }
        protected virtual bool UseBuiltInAnimations { get; } = true; //defaults to true.  however, game of life spinner will do something different.

        [Parameter]
        public string TargetHeight { get; set; } = "";

        [Parameter]
        public string TargetWidth { get; set; } = "";

        [Parameter]
        public float X { get; set; }
        [Parameter]
        public float Y { get; set; }

        [Parameter]
        public SizeF BoardSize { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }



        private string GetSvgStyle()
        {
            if (TargetHeight == "" && TargetWidth == "")
            {
                return "";
            }
            if (TargetHeight != "" && TargetWidth != "")
            {
                return ""; //try this too so you have to pick between 2 options.
            }

            if (TargetHeight != "")
            {
                return $"height: {TargetHeight}";
            }
            return $"width: {TargetWidth}";

        }

        private string GetViewBox()
        {
            return $"0 0 {BoardSize.Width} {BoardSize.Height}";
        }

        protected virtual void Unsubscribe()
        {
            if (UseBuiltInAnimations)
            {
                _aggregator!.Unsubscribe(this, EnumRepaintCategories.Main.ToString());
            }
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        protected void RefreshBoard()
        {
            if (RepaintEventModel.UpdatePartOfBoard != null)
            {
                RepaintEventModel.UpdatePartOfBoard.Invoke();
                return;
            }
            InvokeAsync(() => StateHasChanged());
        }

        void IHandle<RepaintEventModel>.Handle(RepaintEventModel message)
        {
            RefreshBoard();

        }
    }
}