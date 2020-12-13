using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using CommonBasicStandardLibraries.Messenging;
using FlinchCP.Cards;
using FlinchCP.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace FlinchBlazor
{
    public partial class PublicPilesBlazor : IDisposable, IHandleAsync<AnimateCardInPileEventModel<FlinchCardInformation>>
    {
        [CascadingParameter]
        public FlinchVMData? GameData { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        private string RealHeight => TargetHeight.HeightString();
        private enum EnumLocation
        {
            None, Top, Bottom
        }
        private readonly AnimateDeckImageTimer _animates; //had to use this one instead of the new animation i tried but failed to get to work properly.
        public PublicPilesBlazor()
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
        public string AnimationTag { get; set; } = "";
        private SizeF _size;
        protected override void OnInitialized()
        {
            FlinchCardInformation item = new FlinchCardInformation();
            _size = item.DefaultSize;
            Aggregator = cons!.Resolve<IEventAggregator>();
            Aggregator!.Subscribe(this, AnimationTag);
            _animates.StateChanged = ShowChange;
            _animates.LongestTravelTime = 200;
            CommandContainer command = cons.Resolve<CommandContainer>();
            command.AddAction(ShowChange);
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            _topLocation = TargetHeight * -1;
            _bottomLocation = TargetHeight;
            base.OnParametersSet();
        }
        private double _objectLocation { get; set; } = 0;
        private double _topLocation { get; set; }
        private double _bottomLocation { get; set; }
        private BasicPileInfo<FlinchCardInformation>? AnimatePile { get; set; } //i think
        private FlinchCardInformation? CurrentObject { get; set; }
        private int _animateIndex;
        async Task IHandleAsync<AnimateCardInPileEventModel<FlinchCardInformation>>.HandleAsync(AnimateCardInPileEventModel<FlinchCardInformation> message)
        {
            AnimatePile = message.ThisPile;
            _animateIndex = GameData!.PublicPiles.PileList.IndexOf(AnimatePile!);
            CurrentObject = message.ThisCard!;
            switch (message.Direction)
            {
                case EnumAnimcationDirection.StartUpToCard:
                    _animates.LocationYFrom = _topLocation;
                    _animates.LocationYTo = _objectLocation + 1.6;
                    break;
                case EnumAnimcationDirection.StartDownToCard:
                    _animates.LocationYFrom = _bottomLocation;
                    _animates.LocationYTo = _objectLocation;
                    break;
                case EnumAnimcationDirection.StartCardToUp:
                    _animates.LocationYFrom = _objectLocation;
                    _animates.LocationYTo = _topLocation;
                    break;
                case EnumAnimcationDirection.StartCardToDown:
                    _animates.LocationYFrom = _objectLocation;
                    _animates.LocationYTo = _bottomLocation;
                    break;
                default:
                    break;
            }
            await _animates.DoAnimateAsync();
        }
        void IDisposable.Dispose()
        {
            Aggregator!.Unsubscribe(this, AnimationTag);
        }
        private string GetTopText => $"Top: {_animates.CurrentYLocation}vh;";
    }
}