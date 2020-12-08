using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Threading.Tasks;
namespace BasicGamingUIBlazorLibrary.BasicControls.YahtzeeControls
{
    public partial class YahtzeeRowBlazor
    {
        [Parameter]
        public EventCallback RowClicked { get; set; }
        [CascadingParameter]
        public bool IsBottom { get; set; } = false;
        [CascadingParameter]
        public int BottomDescriptionWidth { get; set; } = 500; //kismet will require extras.  hopefully will be easy enough to adjust for kismet or other yahtzee games (?)
        [Parameter]
        public string Header { get; set; } = "";
        [Parameter]
        public string Possible { get; set; } = "";
        [Parameter]
        public string Obtained { get; set; } = "";
        [Parameter]
        public int RowNumber { get; set; }
        [Parameter]
        public bool Highlighted { get; set; }
        private bool MiddleExtra => Header == "Bonus";
        private async Task PrivateClicked()
        {
            await RowClicked!.InvokeAsync(null);
        }
        private PointF GetLocation
        {
            get
            {
                return new PointF(0, (RowNumber - 1) * 200);
            }
        }
        private SizeF GetSize
        {
            get
            {
                if (IsBottom == false)
                {
                    return new SizeF(900, 200);
                }
                float starts = 600;
                return new SizeF(starts + BottomDescriptionWidth, 200);
            }
        }
    }
}