using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.MainRummySets
{
    public partial class BaseIndividualRummySetBlazor<SU, CO, RU, SE, T>
        where SU : Enum
            where CO : Enum
            where RU : class, IRummmyObject<SU, CO>, IDeckObject, new()
            where SE : SetInfo<SU, CO, RU, T>
    {

        protected override bool ShouldRender()
        {
            return DataContext!.HandList.Count == _points.Count;
        }

        [CascadingParameter]
        public BaseMainRummySetsBlazor<SU, CO, RU, SE, T>? MainSet { get; set; }

        [Parameter]
        public RenderFragment<RU>? ChildContent { get; set; }

        [Parameter]
        public SE? DataContext { get; set; }

        [CascadingParameter]
        public int TargetImageHeight { get; set; }
        private string GetSvgStyle()
        {
            RU image = new RU();
            SizeF size = image.DefaultSize;
            var temps = TargetImageHeight * size.Width / size.Height;
            return $"width: {temps}vh"; //hopefully this works.
        }
        private async Task BoardClicked()
        {
            await DataContext!.BoardSingleClickCommand.ExecuteAsync(null);
        }
        private string GetViewBox()
        {
            return $"0 0 {_viewBox.Width} {_viewBox.Height}";
        }
        private CustomBasicList<PointF> _points = new CustomBasicList<PointF>();
        private SizeF _viewBox = new SizeF();
        protected override void OnParametersSet()
        {
            _points = new CustomBasicList<PointF>();
            if (DataContext!.HandList.Count == 0)
            {
                RU image = new RU();
                _viewBox = image.DefaultSize;
            }
            else
            {
                PointF currentPoint = new PointF(0, 0);
                SizeF defaultSize = new SizeF();
                foreach (var hand in DataContext!.HandList)
                {
                    defaultSize = hand.DefaultSize;
                    PointF nextPoint = new PointF(currentPoint.X, currentPoint.Y);
                    _points.Add(nextPoint);
                    double extras;
                    extras = hand.DefaultSize.Height / MainSet!.Divider;
                    extras += MainSet.AdditionalSpacing;
                    currentPoint.Y += (float)extras;
                }
                float maxX = _points.Max(x => x.X);
                float maxY = _points.Max(x => x.Y);
                _viewBox = new SizeF(defaultSize.Width, maxY + defaultSize.Height);
            }
        }   
    }
}