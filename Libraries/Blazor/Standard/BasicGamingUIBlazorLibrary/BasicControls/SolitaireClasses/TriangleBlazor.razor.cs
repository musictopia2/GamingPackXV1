using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.TriangleClasses;
using BasicGamingUIBlazorLibrary.Extensions;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Linq;
namespace BasicGamingUIBlazorLibrary.BasicControls.SolitaireClasses
{
    public partial class TriangleBlazor 
    {
        [Parameter]
        public TriangleObservable? DataContext { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        private string TargetWidth => (TargetHeight * DataContext!.TotalColumns).WidthString<SolitaireCard>();
        private SizeF GetViewSize()
        {
            var card = new SolitaireCard();
            var maxX = DataContext!.CardList.Max(x => x.Location.X);
            maxX += card.DefaultSize.Width;
            var maxY = DataContext.CardList.Max(x => x.Location.Y);
            maxY += card.DefaultSize.Height;
            return new SizeF(maxX, maxY);
        }
    }
}