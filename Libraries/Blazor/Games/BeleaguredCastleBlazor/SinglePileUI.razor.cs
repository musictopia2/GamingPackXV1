using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Threading.Tasks;
namespace BeleaguredCastleBlazor
{
    public partial class SinglePileUI
    {
        [CascadingParameter]
        public SolitairePilesCP? MainPiles { get; set; }
        [Parameter]
        public PileInfoCP? SinglePile { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        private readonly CustomBasicList<PointF> _points = new CustomBasicList<PointF>();
        protected override void OnParametersSet()
        {
            RecalculatePositions();
        }
        private SizeF _viewBox = new SizeF();

        private string GetViewBox()
        {
            return $"0 0 {_viewBox.Width} {_viewBox.Height}";
        }
        private async Task Submit()
        {
            if (MainPiles!.ColumnCommand.CanExecute(SinglePile) == false)
            {
                return;
            }
            await MainPiles.ColumnCommand.ExecuteAsync(SinglePile);
        }
        private void RecalculatePositions()
        {
            _points.Clear(); //try this way.
            SolitaireCard image = new SolitaireCard();
            float tempWidth = image.DefaultSize.Width * 6;
            SinglePile!.CardList.UnselectAllObjects();
            _viewBox = new SizeF(tempWidth, image.DefaultSize.Height);
            PointF currentPoint = new PointF(0, 0);
            SizeF defaultSize = image.DefaultSize;
            double extras;
            foreach (var card in SinglePile.CardList)
            {
                PointF nextPoint = new PointF(currentPoint.X, currentPoint.Y);
                _points.Add(nextPoint);
                extras = defaultSize.Width / 4;
                currentPoint.X += (float)extras;
            }
            _points.Reverse();//has to reverse this time.
        }
    }
}