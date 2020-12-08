using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.SolitaireClasses
{
    public partial class MainClockBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public ClockObservable? DataContext { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private string GetPositionStyle(ClockInfo clock)
        {
            SolitaireCard card = new SolitaireCard();
            double percentages = TargetHeight / card.DefaultSize.Height;
            double heights = percentages * clock.Location.Y;
            double widths = percentages * clock.Location.X;
            string output = $"Top: {heights}vh; Left: {widths}vh;";
            return output;
        }
    }
}