using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.MultipleFrameContainers
{
    public partial class PrivateBasicIndividualPileBlazor<D>
        where D : class, IDeckObject, new()
    {
        [CascadingParameter]
        public BasicMultiplePilesBlazor<D>? MainGroup { get; set; } //can get all the details from the maingroup which is good.
        [Parameter]
        public BasicPileInfo<D>? IndividualPile { get; set; } //decided to call it an indexpile.
        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public RenderFragment<D>? ChildContent { get; set; }
        private bool IsEnabled
        {
            get
            {
                if (IndividualPile!.IsEnabled == false)
                {
                    return false;
                }
                if (MainGroup!.Piles!.IsEnabled == false)
                {
                    return false;
                }
                return true;
            }
        }

        private D? _card;
        protected override void OnParametersSet()
        {
            _card = GetCard();
            _card.IsSelected = IndividualPile!.IsSelected;
            base.OnParametersSet();
        }
        private D GetCard()
        {
            if (ShowPrevious() == false)
            {
                return MainGroup!.Piles!.GetLastCard(IndividualPile!);
            }
            return MainGroup!.RenderCard!;
        }

        private bool ShowPrevious()
        {
            if (MainGroup!.ShowPrevious == true)
            {
                return false;
            }
            if (MainGroup.RenderCard == null)
            {
                return false;
            }
            if (MainGroup.AnimatePile == null)
            {
                return false;
            }
            if (MainGroup.AnimatePile.Column == IndividualPile!.Column && MainGroup.AnimatePile.Row == IndividualPile.Row)
            {
                return true;
            }
            return false;
        }

        private string GetViewBox(D card)
        {
            return $"0 0 {card.DefaultSize.Width} {card.DefaultSize.Height}";
        }
        private string GetString => TargetHeight.HeightString();

        protected override bool ShouldRender()
        {
            return MainGroup!.CanRender();
        }
    }
}