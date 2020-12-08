using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.SingleMiscPiles
{
    public partial class BaseSingleMiscPileBlazor<D> : IDisposable, IHandleAsync<AnimateCardInPileEventModel<D>>, IHandle<ResetCardsEventModel>
        where D : class, IDeckObject, new()
    {
        private enum EnumLocation
        {
            None, Top, Bottom
        }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<D>? SinglePile { get; set; }



        [Parameter]
        public string PileAnimationTag { get; set; } = "maindiscard";
        private readonly AnimateDeckImageTimer _animates; //misleading name.  may think about better name for next version.
        public BaseSingleMiscPileBlazor()
        {
            _animates = new AnimateDeckImageTimer();
        }
        private void ShowChange()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        [Parameter]
        public RenderFragment<D>? CanvasTemplate { get; set; } //can't use generics because that control is responsible for knowing which one it is (via event aggregation).
        [Parameter]
        public RenderFragment<D>? MainTemplate { get; set; }
        private IEventAggregator? Aggregator { get; set; }
        public void Dispose()
        {
            Aggregator!.Unsubscribe(this, PileAnimationTag);
            Aggregator.Unsubscribe(this);
            CommandContainer command = cons!.Resolve<CommandContainer>();
            command.RemoveAction(ShowChange);
        }
        protected override void OnInitialized()
        {
            Aggregator = cons!.Resolve<IEventAggregator>();
            Aggregator!.Subscribe(this, PileAnimationTag);
            Aggregator.Subscribe(this);
            _animates.StateChanged = ShowChange;
            _animates.LongestTravelTime = 200;
            CommandContainer command = cons.Resolve<CommandContainer>();
            command.AddAction(ShowChange);
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            TopLocation = TargetHeight * -1;
            BottomLocation = TargetHeight;
            base.OnParametersSet();
        }
        private bool _showPrevious;
        private D? AltShowImage { get; set; }
        private D? AnimateDeckImage { get; set; }
        private double ObjectLocation { get; set; } = 0;
        private double TopLocation { get; set; }
        private double BottomLocation { get; set; } //if its 8, then has to figure out what else to do (?)
        async Task IHandleAsync<AnimateCardInPileEventModel<D>>.HandleAsync(AnimateCardInPileEventModel<D> message)
        {
            _showPrevious = false;
            AltShowImage = null;
            AnimateDeckImage = message.ThisCard;
            AnimateDeckImage!.IsSelected = false; //just in case.
            switch (message.Direction)
            {
                case EnumAnimcationDirection.StartUpToCard:
                    _animates.LocationYFrom = TopLocation;
                    _animates.LocationYTo = ObjectLocation;
                    break;
                case EnumAnimcationDirection.StartDownToCard:
                    _animates.LocationYFrom = BottomLocation;
                    _animates.LocationYTo = ObjectLocation;

                    break;
                case EnumAnimcationDirection.StartCardToUp:
                    _showPrevious = true;
                    //_removeCard = true;
                    AltShowImage = SinglePile!.CurrentCard;
                    _animates.LocationYFrom = ObjectLocation;
                    _animates.LocationYTo = TopLocation;
                    break;
                case EnumAnimcationDirection.StartCardToDown:
                    _showPrevious = true;
                    //_removeCard = true;
                    AltShowImage = SinglePile!.CurrentCard;
                    _animates.LocationYFrom = ObjectLocation;
                    _animates.LocationYTo = BottomLocation;
                    break;
                default:
                    break;
            }
            await _animates.DoAnimateAsync();
        }
        private void Reset()
        {
            AltShowImage = null;
            AnimateDeckImage = null;
        }

        void IHandle<ResetCardsEventModel>.Handle(ResetCardsEventModel message)
        {
            Reset();
        }

        private string GetTopText => $"Top: {_animates.CurrentYLocation}vh;";
    }
}