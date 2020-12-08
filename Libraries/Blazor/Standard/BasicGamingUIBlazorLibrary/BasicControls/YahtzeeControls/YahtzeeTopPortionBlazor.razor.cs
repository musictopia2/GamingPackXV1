using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.YahtzeeControls
{
    public partial class YahtzeeTopPortionBlazor
    {
        [Parameter]
        public CustomBasicList<RowInfo> TopList { get; set; } = new CustomBasicList<RowInfo>();
        [Parameter]
        public RowInfo? BonusInfo { get; set; }
        [Parameter]
        public RowInfo? TopScore { get; set; }
        private int GetRow(RowInfo row) => TopList.IndexOf(row) + 2;
        [Parameter]
        public EventCallback<RowInfo> RowClicked { get; set; } //something else should handle this one.
    }
}