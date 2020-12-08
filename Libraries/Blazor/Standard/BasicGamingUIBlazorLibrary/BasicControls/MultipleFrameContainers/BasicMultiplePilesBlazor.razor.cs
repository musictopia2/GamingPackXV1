using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.MultipleFrameContainers
{
    public partial class BasicMultiplePilesBlazor<D> : IDisposable, IHandleAsync<AnimateCardInPileEventModel<D>>, IHandle<ResetCardsEventModel>
        where D : class, IDeckObject, new()
    {
        private enum EnumLocation
        {
            None, Top, Bottom
        }
        private readonly AnimateDeckImageTimer _animates;
        public BasicMultiplePilesBlazor()
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
        private IEventAggregator? Aggregator { get; set; }

        [Parameter]
        public BasicMultiplePilesCP<D>? Piles { get; set; }

        [Parameter]
        public string AnimationTag { get; set; } = ""; //you must have one.  otherwise can't show either.

        //maybe we don't need this anymore (?)

        //protected override bool ShouldRender()
        //{
        //    if (_animates.CurrentYLocation == -1000 && EventExtensions.AnimationCompleted == false && _removeCard == false)
        //    {
        //        return false;
        //    }
        //    return base.ShouldRender();
        //}

        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public bool Inline { get; set; } = true;
        protected override void OnInitialized()
        {
            D item = new D();
            Aggregator = cons!.Resolve<IEventAggregator>();
            Aggregator!.Subscribe(this, AnimationTag);
            Aggregator.Subscribe(this); //try this too.
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
        [Parameter]
        public RenderFragment<D>? CanvasTemplate { get; set; } //can't use generics because that control is responsible for knowing which one it is (via event aggregation).
        [Parameter]
        public RenderFragment<BasicPileInfo<D>>? MainTemplate { get; set; } //because of cascading, hopefully the child can render what is needed.
        [Parameter]
        public RenderFragment? MiscRowTemplate { get; set; }
        [Parameter]
        public int RowPlaceAfter { get; set; }
        private int GetRowCount
        {
            get
            {
                if (MiscRowTemplate == null || RowPlaceAfter == 0)
                {
                    return Piles!.Rows;
                }
                return Piles!.Rows + 1;
            }
        }
        private double ObjectLocation { get; set; } = 0;
        private double TopLocation { get; set; }
        private double BottomLocation { get; set; } //if its 8, then has to figure out what else to do (?)
        private BasicPileInfo<D>? GetPile(int column, int row)
        {
            return Piles!.PileList!.SingleOrDefault(x => x.Column == column && x.Row == row); //maybe we have it and maybe we don't
        }
        internal BasicPileInfo<D>? AnimatePile { get; set; }
        private D? CurrentObject { get; set; }

        internal bool ShowPrevious { get; set; }

        internal D? RenderCard { get; set; }

        internal bool CanRender()
        {
            return _animates.CurrentYLocation == -1000 || ShowPrevious == true;
        }

        //try to follow the same idea.

        private D? AltShowImage { get; set; }
        private D? AnimateDeckImage { get; set; }

        private void PopulateCardToShow()
        {
            if (AltShowImage != null)
            {
                RenderCard = AltShowImage;
            }
            else if (AnimateDeckImage != null)
            {
                RenderCard = AnimateDeckImage;
            }
            else
            {
                RenderCard = null;
            }
        }

        async Task IHandleAsync<AnimateCardInPileEventModel<D>>.HandleAsync(AnimateCardInPileEventModel<D> message)
        {
            ShowPrevious = false; //just in case.
            AnimatePile = message.ThisPile;
            AltShowImage = null;
            AnimateDeckImage = message.ThisCard;
            CurrentObject = message.ThisCard!;
            CurrentObject.IsSelected = false; //just in case.
            switch (message.Direction)
            {
                case EnumAnimcationDirection.StartUpToCard:
                    _animates.LocationYFrom = TopLocation;
                    _animates.LocationYTo = ObjectLocation;

                    break;
                case EnumAnimcationDirection.StartDownToCard:
                    _animates.LocationYFrom = BottomLocation;
                    _animates.LocationYTo = ObjectLocation + 1.7; //only y alone.
                    break;
                case EnumAnimcationDirection.StartCardToUp:
                    ShowPrevious = true;
                    AltShowImage = Piles!.GetLastCard(message.ThisPile!);
                    _animates.LocationYFrom = ObjectLocation;
                    _animates.LocationYTo = TopLocation;
                    break;
                case EnumAnimcationDirection.StartCardToDown:
                    ShowPrevious = true;
                    AltShowImage = Piles!.GetLastCard(message.ThisPile!);
                    _animates.LocationYFrom = ObjectLocation;
                    _animates.LocationYTo = BottomLocation;
                    break;
                default:
                    break;
            }
            Console.WriteLine(ShowPrevious);
            await _animates.DoAnimateAsync();
        }
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        void IDisposable.Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            Aggregator!.Unsubscribe(this, AnimationTag);
            Aggregator.Unsubscribe(this);
            CommandContainer command = cons!.Resolve<CommandContainer>();
            command.RemoveAction(ShowChange);
        }

        void IHandle<ResetCardsEventModel>.Handle(ResetCardsEventModel message)
        {
            AltShowImage = null;
            AnimateDeckImage = null;
            AnimatePile = null; //try this too (?)
        }

        private string GetTopText => $"Top: {_animates.CurrentYLocation}vh; Left: 2px;";
    }
}