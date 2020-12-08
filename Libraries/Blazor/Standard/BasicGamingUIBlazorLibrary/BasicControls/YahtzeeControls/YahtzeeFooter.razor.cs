using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.YahtzeeControls
{
    public partial class YahtzeeFooter
    {
        [Parameter]
        public string Text { get; set; } = "";
        [Parameter]
        public string TotalScore { get; set; } = ""; //needs to be string after all.
        [Parameter]
        public int RowNumber { get; set; }
    }
}