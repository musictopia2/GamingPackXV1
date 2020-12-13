using FourSuitRummyCP.Data;
using Microsoft.AspNetCore.Components;
namespace FourSuitRummyBlazor.Views
{
    public partial class PlayerSetsView
    {
        [Parameter]
        public FourSuitRummyPlayerItem? PlayerUsed { get; set; }
        [CascadingParameter]
        public FourSuitRummyGameContainer? GameContainer { get; set; }
    }
}