using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.DrawPiles
{
    public partial class BaseDrawPileBlazor<D> : IDisposable, IHandleAsync<AnimateCardInPileEventModel<D>>
         where D : class, IDeckObject, new()
    {
        private enum EnumLocation
        {
            None, Top, Bottom
        }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15; //can experiment to see what the default should be.
        [Parameter]
        public DeckObservablePile<D>? DeckPile { get; set; }
        [Parameter]
        public string DeckAnimationTag { get; set; } = "maindeck";
        private readonly AnimateDeckImageTimer _animates;
        public BaseDrawPileBlazor()
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
            Aggregator!.Unsubscribe(this, DeckAnimationTag);
            CommandContainer command = cons!.Resolve<CommandContainer>();
            command.RemoveAction(ShowChange);
        }
        private float GetHalfTop()
        {
            var firsts = _defaultSize.Width * TargetHeight;
            return firsts / _defaultSize.Height;
        }
        private string GetMarginsForTextCenter()
        {
            float tops = GetHalfTop();
            return $"margin-top: -{tops}vh"; //hopefully this works.
        }
        private SizeF _defaultSize;
        protected override void OnInitialized()
        {
            D card = new D();
            _defaultSize = card.DefaultSize;
            Aggregator = cons!.Resolve<IEventAggregator>();
            Aggregator!.Subscribe(this, DeckAnimationTag);
            _animates.StateChanged = ShowChange; //tried the statechanged but still did not work.
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
        private D? AnimateDeckImage { get; set; }
        private double ObjectLocation { get; set; } = 0;
        private double TopLocation { get; set; }
        private double BottomLocation { get; set; } //if its 8, then has to figure out what else to do (?)
        async Task IHandleAsync<AnimateCardInPileEventModel<D>>.HandleAsync(AnimateCardInPileEventModel<D> message)
        {
            var card = new D();
            card.Populate(message.ThisCard!.Deck);
            if (DeckPile!.DeckStyle == EnumDeckPileStyle.AlwaysKnown)
            {
                card.IsUnknown = false;
            }
            else
            {
                card.IsUnknown = true;
            }
            AnimateDeckImage = card;
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
                    _animates.LocationYFrom = ObjectLocation;
                    _animates.LocationYTo = TopLocation;
                    break;
                case EnumAnimcationDirection.StartCardToDown:
                    _animates.LocationYFrom = ObjectLocation;
                    _animates.LocationYTo = BottomLocation;
                    break;
                default:
                    break;
            }
            await _animates.DoAnimateAsync();
        }
        private string GetTopText => $"Top: {_animates.CurrentYLocation}vh;";
    }
}