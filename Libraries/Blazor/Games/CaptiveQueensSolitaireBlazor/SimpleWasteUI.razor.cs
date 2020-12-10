using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using CaptiveQueensSolitaireCP.Logic;
using CaptiveQueensSolitaireCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace CaptiveQueensSolitaireBlazor
{
    public partial class SimpleWasteUI
    {
        [CascadingParameter]
        public CaptiveQueensSolitaireMainViewModel? DataContext { get; set; }
        private CustomWaste? Waste { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private SizeF _viewBox = new SizeF();
        private readonly CustomBasicList<PointF> _points = new CustomBasicList<PointF>();
        protected override void OnParametersSet()
        {
            Waste = (CustomWaste)DataContext!.WastePiles1;
            if (Waste.CardList.Count != 4)
            {
                return;
            }
            _points.Clear();
            var card = new SolitaireCard();
            var firstSize = card.DefaultSize;
            var tempSize = firstSize.Height * .67f;
            _points.Add(new PointF(0, tempSize)); //needs lots of experimenting for the positioning unfortunately.
            _points.Add(new PointF(tempSize, firstSize.Height + 10));
            _points.Add(new PointF(firstSize.Height, tempSize)); //more iffy.
            _points.Add(new PointF(tempSize, 0)); //most iffy.  hopefully can do trial/error to get it correct but no guarantees.
            _viewBox.Height = card.DefaultSize.Height * 2;
            _viewBox.Width = _viewBox.Height;
            base.OnParametersSet();
        }
    }
}