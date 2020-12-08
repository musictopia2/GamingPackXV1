using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.SimpleControls
{
    public partial class BaseFrameBlazor
    {
        //since its intended for others to put into it, no parameters.
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public string TargetWidth { get; set; } = "";
        [Parameter]
        public int PaddingHeight { get; set; } = 0;
        [Parameter]
        public string Text { get; set; } = ""; //they have to populate the text as well.
        [Parameter]
        public bool IsEnabled { get; set; } = true;
        [Parameter]
        public bool FullFrame { get; set; } = false;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private bool IsDisabled => !IsEnabled;
        private string GetColorStyle()
        {
            if (IsDisabled == false)
            {
                return "";
            }
            return $"color:{cc.LightGray.ToWebColor()}; border-color: {cc.LightGray.ToWebColor()}";
        }
        private string GetContainerStyle()
        {
            if (TargetHeight == "" || TargetWidth == "")
            {
                return "";
            }
            if (TargetWidth == "")
            {
                return $"height: {TargetHeight};";
            }
            if (TargetHeight == "")
            {
                return $"width: {TargetWidth};";
            }
            return $"height: {TargetHeight};width: {TargetWidth};";
        }
        private string PaddingStyle()
        {
            if (PaddingHeight > 0)
            {
                return $"margin-right: {PaddingHeight}px";
            }
            return "";
        }
        private string FrameClass
        {
            get
            {
                if (FullFrame)
                {
                    return "fullframe";
                }
                return "";
            }
        }
        //i have to take a risk.
        //because when i use the @key, that is causing many issues.  obviously i can wrap the frame with a key if necessary as well.

        //good news is taking out the key fixed slightly the performance issues.
        //if i need a key, requires thinking case by case now.

    }
}