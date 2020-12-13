using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace ThinkTwiceBlazor
{
    public partial class ButtonDiceBlazor
    {
        [CascadingParameter]
        public string TargetHeight { get; set; } = ""; //try this way this time.
        [Parameter]
        public bool WillHold { get; set; }
        [Parameter]
        public string Value { get; set; } = "";
        [Parameter]
        public EventCallback ButtonClicked { get; set; }
        private async Task PrivateClicked()
        {
            if (ButtonClicked.HasDelegate == false)
            {
                return;
            }
            await ButtonClicked.InvokeAsync(null);
        }
    }
}