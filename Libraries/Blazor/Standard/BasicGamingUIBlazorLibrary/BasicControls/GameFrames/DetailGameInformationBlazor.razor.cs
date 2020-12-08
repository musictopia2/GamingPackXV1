using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameFrames
{
    public partial class DetailGameInformationBlazor
    {
        [Parameter]
        public CustomBasicList<LabelGridModel> Labels { get; set; } = new CustomBasicList<LabelGridModel>();

        [Parameter]
        public object? DataContext { get; set; } //may have a better way of doing this.

        [Parameter]
        public string Width { get; set; } = "";

        [Parameter]
        public string Height { get; set; } = "";
        [Parameter]
        public string Text { get; set; } = "Additional Information";
    }
}