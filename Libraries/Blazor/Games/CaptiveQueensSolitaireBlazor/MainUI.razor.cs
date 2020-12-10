using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGamingUIBlazorLibrary.Extensions;
using CaptiveQueensSolitaireCP.Logic;
using CaptiveQueensSolitaireCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace CaptiveQueensSolitaireBlazor
{
    public partial class MainUI
    {
        [CascadingParameter]
        public CaptiveQueensSolitaireMainViewModel? DataContext { get; set; }
        [Parameter]
        public CustomMain? Main { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        private readonly CustomBasicList<PointF> _points = new CustomBasicList<PointF>();
        private PointF _queenLocation;
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        private SizeF _viewSize;
        protected override void OnParametersSet()
        {
            if (DataContext == null || Main == null)
            {
                return;
            }
            if (Main.PileList == null)
            {
                return;
            }
            _points.Clear();
            var card = new SolitaireCard();
            var size = card.DefaultSize;
            _queenLocation = new PointF(size.Width + 50, size.Height);
            _points.Add(new PointF(size.Width + 50 + 60, 0));
            _points.Add(new PointF(size.Width + size.Height + 60, 0));
            _points.Add(new PointF(size.Width * 3.9f, size.Height + 60));
            _points.Add(new PointF(size.Width * 3.9f, size.Height + size.Width + 60));
            _points.Add(new PointF(size.Width + 50 + 60, (size.Height * 3) + 10));
            _points.Add(new PointF(size.Width + size.Height + 60, (size.Height * 3) + 10));
            _points.Add(new PointF(0, size.Height + 60));
            _points.Add(new PointF(0, size.Height + size.Width + 60));
            _viewSize = new SizeF(size.Width * 6, size.Height * 4.5f);
            base.OnParametersSet();
        }
        private string TargetWidth => (TargetHeight * 6).WidthString<SolitaireCard>();
        private string GetViewBox()
        {
            return $"0 0 {_viewSize.Width} {_viewSize.Height}";
        }
    }
}