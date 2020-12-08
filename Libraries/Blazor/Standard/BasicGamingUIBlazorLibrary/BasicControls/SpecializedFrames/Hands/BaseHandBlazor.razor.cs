using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.Hands
{
    public partial class BaseHandBlazor<D>
        where D : class, IDeckObject, new()
    {

        protected override bool ShouldRender()
        {
            return _points.Count == Hand!.HandList.Count;
        }

        [Parameter]
        public bool Rotated { get; set; } = false; //maybe best to do this way.  not too often its rotated anyways.
        [Parameter]
        public RenderFragment<D>? ChildContent { get; set; }
        [Parameter]
        public HandObservable<D>? Hand { get; set; }
        [Parameter]
        public EnumHandList HandType { get; set; } = EnumHandList.Horizontal;
        [Parameter]
        public int PaddingRight { get; set; } = 0; //will be in pixels this time.
        [Parameter]
        public double Divider { get; set; } = 1;
        [Parameter]
        public double AdditionalSpacing { get; set; } = -5;
        //we are doing to do a viewbox.
        /// <summary>
        /// this is where you usually set a percentage which represents how high or wide the container is.
        /// if hand type is horizontal, then its the width
        /// otherwise, its the height
        /// </summary>
        [Parameter]
        public string TargetContainerSize { get; set; } = ""; //if not set, will keep going forever.
        /// <summary>
        /// this is where you set the percentage which represents how big the images are
        /// if horizontal is used, then its the height.  otherwise, its the width.
        /// 
        /// </summary>
        [Parameter]
        public string TargetImageSize { get; set; } = "";
        [CascadingParameter]
        public int TargetImageHeight { get; set; }
        private bool IsDisabled => !Hand!.IsEnabled;
        private string GetContainerStyle()
        {
            if (TargetContainerSize == "")
            {
                if (HandType == EnumHandList.Horizontal)
                {
                    return $"overflow-x: auto; margin-right: 10px;";
                }
                return $"overflow-y: auto; margin-bottom: 10px; padding-right: {PaddingRight}px;";
            }
            if (HandType == EnumHandList.Horizontal)
            {
                return $"width: {TargetContainerSize}; overflow-x: auto;";
            }
            return $"height: {TargetContainerSize}; overflow-y: auto; padding-right: {PaddingRight}px;";
        }
        private string GetSvgStyle()
        {

            if (HandType == EnumHandList.Horizontal && TargetImageHeight > 0)
            {
                return $"height: {TargetImageHeight}vh";
            }
            if (HandType == EnumHandList.Vertical && TargetImageHeight > 0)
            {
                //figure out what it is.  has to use proportions.
                D image = new D();
                SizeF size = image.DefaultSize;
                var temps = TargetImageHeight * size.Width / size.Height;
                return $"width: {temps}vh"; //hopefully this works.
            }
            if (TargetImageSize == "")
            {
                return "";
            }
            if (HandType == EnumHandList.Horizontal)
            {

                return $"height: {TargetImageSize}";
            }
            return $"width: {TargetImageSize}";
        }
        private async Task BoardClicked()
        {
            await Hand!.BoardSingleClickCommand.ExecuteAsync(null);
        }
        private string GetViewBox()
        {
            return $"0 0 {_viewBox.Width} {_viewBox.Height}";
        }
        private CustomBasicList<PointF> _points = new CustomBasicList<PointF>();
        private SizeF _viewBox = new SizeF();
        private string GetColorStyle()
        {
            if (IsDisabled == false)
            {
                return "";
            }
            return $"color:{cc.LightGray.ToWebColor()}; border-color: {cc.LightGray.ToWebColor()}";
        }
        private void CalculateHandMax()
        {
            var currentPoint = new PointF(0, 0);
            D card = new D();
            var defaultSize = card.DefaultSize;
            double extras = 0;
            for (int i = 0; i < Hand!.Maximum; i++)
            {
                if (HandType == EnumHandList.Horizontal)
                {
                    extras = defaultSize.Width / Divider;
                    extras += AdditionalSpacing;
                    currentPoint.X += (float)extras;
                }
                else
                {
                    extras = defaultSize.Height / Divider;
                    extras += AdditionalSpacing;

                    currentPoint.Y += (float)extras;
                }
            }
            if (HandType == EnumHandList.Horizontal)
            {
                currentPoint.X += (float)extras;
                _viewBox = new SizeF(currentPoint.X - defaultSize.Width + 10, defaultSize.Height);
            }
            else
            {
                currentPoint.Y += (float)extras;
                _viewBox = new SizeF(defaultSize.Width - defaultSize.Height, currentPoint.Y);
            }
        }
        protected override void OnParametersSet()
        {
            _points = new CustomBasicList<PointF>();
            if (Hand!.HandList.Count == 0)
            {
                D image = new D();
                if (Hand.Maximum == 0)
                {
                    SizeF size = new SizeF(image.DefaultSize.Width * 2, image.DefaultSize.Height);
                    _viewBox = size;
                }
                else
                {
                    CalculateHandMax();
                }
            }
            else
            {
                PointF currentPoint = new PointF(0, 0);
                SizeF defaultSize = new SizeF();
                foreach (var hand in Hand.HandList)
                {
                    defaultSize = hand.DefaultSize;
                    PointF nextPoint = new PointF(currentPoint.X, currentPoint.Y);
                    _points.Add(nextPoint);
                    double extras;
                    if (Rotated == false)
                    {
                        if (HandType == EnumHandList.Horizontal)
                        {
                            extras = hand.DefaultSize.Width / Divider;
                            extras += AdditionalSpacing;
                            currentPoint.X += (float)extras;
                        }
                        else
                        {
                            extras = hand.DefaultSize.Height / Divider;
                            extras += AdditionalSpacing;

                            currentPoint.Y += (float)extras;
                        }
                    }
                    else
                    {
                        if (HandType == EnumHandList.Horizontal)
                        {
                            extras = hand.DefaultSize.Height / Divider;
                            extras += AdditionalSpacing;
                            currentPoint.X += (float)extras;
                        }
                        else
                        {
                            extras = hand.DefaultSize.Width / Divider;
                            extras += AdditionalSpacing;

                            currentPoint.Y += (float)extras;
                        }
                    }
                }
                if (Hand.Maximum == 0)
                {
                    float maxX;
                    if (Hand.HandList.Count == 1)
                    {
                        maxX = defaultSize.Width;
                    }
                    else
                    {
                        maxX = _points.Max(x => x.X);
                    }
                    float maxY = _points.Max(x => x.Y);
                    if (Rotated == false)
                    {
                        if (HandType == EnumHandList.Horizontal)
                        {
                            _viewBox = new SizeF(maxX + defaultSize.Width, defaultSize.Height);
                        }
                        else
                        {
                            _viewBox = new SizeF(defaultSize.Width, maxY + defaultSize.Height);
                        }
                    }
                    else
                    {
                        if (HandType == EnumHandList.Horizontal)
                        {
                            _viewBox = new SizeF(maxX + defaultSize.Height, defaultSize.Width);
                        }
                        else
                        {
                            _viewBox = new SizeF(defaultSize.Height, maxY + defaultSize.Width);
                        }
                    }
                }
                else
                {
                    CalculateHandMax();
                }
            }
        }
    }
}