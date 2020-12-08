using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.YahtzeeControls
{
    public partial class YahtzeeBottomPortionBlazor
    {
        [CascadingParameter]
        public int BottomDescriptionWidth { get; set; } = 500; //kismet will require extras.  hopefully will be easy enough to adjust for kismet or other yahtzee games (?)
        [Parameter]
        public CustomBasicList<RowInfo> BottomList { get; set; } = new CustomBasicList<RowInfo>();
        [Parameter]
        public RowInfo? BottomScore { get; set; }
        private int GetRow(RowInfo row) => BottomList.IndexOf(row) + 2;
        [Parameter]
        public EventCallback<RowInfo> RowClicked { get; set; } //something else should handle this one.
        private int CompleteBottomWidth
        {
            get
            {
                return (300 * 2) + BottomDescriptionWidth;
            }
        }
        private int CompleteBottomHeight
        {
            get
            {
                return (BottomList.Count + 2) * 200;
            }
        }
        private string GetViewBox
        {
            get
            {
                string output = $"0 0 {CompleteBottomWidth} {CompleteBottomHeight}";
                return output;
            }
        }
        private int GetBottomRow => BottomList.Count + 2;
    }
}