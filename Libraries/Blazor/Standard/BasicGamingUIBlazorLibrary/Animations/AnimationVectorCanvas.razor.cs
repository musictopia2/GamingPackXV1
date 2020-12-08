using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGamingUIBlazorLibrary.BasicControls.GameBoards;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.Animations
{
    public partial class AnimationVectorCanvas<S> : IHandleAsync<AnimatePieceEventModel<S>>, IDisposable //this requires generics since the other did and we are cascading to use the stuff from the parent board.
        where S : class, IBasicSpace, new()
    {
        private bool _disposedValue;
        [CascadingParameter]
        public GridGameBoard<S>? MainBoard { get; set; }
        [Parameter]
        public RenderFragment<S>? ChildContent { get; set; }
        private IEventAggregator? Aggregator { get; set; }
        public AnimationVectorCanvas()
        {
            _animates = new AnimateGrid();
        }
        protected override void OnInitialized()
        {
            //had to use off the shelf for sample but not here.
            Aggregator = cons!.Resolve<IEventAggregator>();
            Aggregator!.Subscribe(this);
            _animates.StateChanged = ShowChange;
            _animates.LongestTravelTime = 200;
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        private S? AnimatePiece { get; set; } //this can be iffy now.
        private AnimateGrid _animates;
        async Task IHandleAsync<AnimatePieceEventModel<S>>.HandleAsync(AnimatePieceEventModel<S> message)
        {
            AnimatePiece = message.TemporaryObject!;
            if (AnimatePiece is ISelectableObject selects)
            {
                selects.IsSelected = false; //set to false.
            }
            _animates.LocationFrom = MainBoard!.GetControlLocation(message.PreviousSpace.Row, message.PreviousSpace.Column);
            _animates.LocationTo = MainBoard.GetControlLocation(message.MoveToSpace.Row, message.MoveToSpace.Column);
            await _animates.DoAnimateAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Aggregator!.Unsubscribe(this);
                    _animates.StateChanged = null;
                    _animates = default!;
                }

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}