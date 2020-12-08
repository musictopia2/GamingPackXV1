using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.SolitaireClasses
{
    public partial class IndividualSolitairePileBlazor
    {

        //hint.  individual solitaire pile did not have the issue.

        //if i set the shouldrender to false, good news is no performance problems.
        //the bad news is even selecting something does not work though.

        //try this way.
        protected override bool ShouldRender()
        {
            return _points.Count == SinglePile!.CardList.Count;
        }

        private string CustomKey => $"IndividualSolitairePile{SolitairePilesCP.DealNumber},{MainPiles!.PileList.IndexOf(SinglePile!)}";

        [CascadingParameter]
        public SolitairePilesCP? MainPiles { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public PileInfoCP? SinglePile { get; set; }
        private readonly CustomBasicList<PointF> _points = new CustomBasicList<PointF>();
        protected override void OnParametersSet()
        {
            RecalculatePositions();
        }
        private void RecalculatePositions()
        {
            _points.Clear(); //try this way.
            if (SinglePile!.CardList.Count == 0)
            {
                SolitaireCard image = new SolitaireCard();
                _viewBox = image.DefaultSize;
            }
            else
            {
                PointF currentPoint = new PointF(0, 0);
                SizeF defaultSize = new SizeF();
                double extras;
                foreach (var card in SinglePile.CardList)
                {
                    defaultSize = card.DefaultSize;
                    PointF nextPoint = new PointF(currentPoint.X, currentPoint.Y);
                    _points.Add(nextPoint);
                    if (card.IsUnknown == false || MainPiles!.IsKlondike == false)
                    {
                        extras = defaultSize.Height / 4;
                    }
                    else
                    {
                        extras = defaultSize.Height * .12; //can experiment.
                    }
                    extras += -5; //no margins needed for this.
                    currentPoint.Y += (float)extras;
                }
                _viewBox = new SizeF(defaultSize.Width, currentPoint.Y + defaultSize.Height);
            }
        }
        public string GetWidth => TargetHeight.WidthString<SolitaireCard>(); //no matter what.
        private SizeF _viewBox = new SizeF();
        private string GetViewBox()
        {
            return $"0 0 {_viewBox.Width} {_viewBox.Height}";
        }
        private string GetFill()
        {
            if (SinglePile!.IsSelected)
            {
                return cc.Red.ToWebColor();
            }
            return cc.Transparent.ToWebColor(); //hopefully this is fine.
        }
        private async Task ClickSinglePile() //for now, don't worry about double clicking.
        {
            if (MainPiles!.ColumnCommand.CanExecute(SinglePile) == false)
            {
                return;
            }
            await MainPiles.ColumnCommand.ExecuteAsync(SinglePile);
        }
    }
}