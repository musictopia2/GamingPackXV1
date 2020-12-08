using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.TrickUIs
{
    public partial class BaseSeveralPlayersTrickBlazor<P, B, S, T> : IHandleAsync<AnimateTrickEventModel>, IDisposable
        where P : IPlayerTrick<S, T>, new()
        where B : BasicTrickAreaObservable<S, T>, IMultiplayerTrick<S, T, P>
        where S : struct, Enum
        where T : class, ITrickCard<S>, new()
    {

        [CascadingParameter]
        public int TargetHeight { get; set; } = 15; //its the view height.
        [Parameter]
        public RenderFragment<T>? ChildContent { get; set; } //this is the card that will actually appear.
        [Parameter]
        public B? DataContext { get; set; }
        [Parameter]
        public bool ExtraLongSecondColumn { get; set; } = false;
        private readonly AnimateTrickClass<S, T> _animates;
        private IEventAggregator? Aggregator { get; set; }
        private SizeF _tempSize;
        public BaseSeveralPlayersTrickBlazor()
        {
            _animates = new AnimateTrickClass<S, T>();
            _animates.LongestTravelTime = 200; //can adjust as needed.
            _animates.StateChanged = () => InvokeAsync(StateHasChanged);
            _animates.GetStartingPoint = GetStartingPoint;
            T card = new T();
            _tempSize = card.DefaultSize; //for proportions.
        }
        private PointF GetStartingPoint(int index)
        {
            TrickCoordinate trick = DataContext!.ViewList![index]; //i think
            int x;
            if (trick.Column == 1)
            {
                x = 0;
            }
            else
            {
                x = GetLeft(trick.Column);
            }
            int y = GetCardTop(trick.Row);
            return new PointF(x, y); //for now, this is fine (?)
        }
        void IDisposable.Dispose()
        {
            Aggregator!.Unsubscribe(this);
        }
        async Task IHandleAsync<AnimateTrickEventModel>.HandleAsync(AnimateTrickEventModel message)
        {
            if (DataContext == null)
            {
                return;
            }
            await _animates.MovePiecesAsync(DataContext);
        }
        protected override void OnInitialized()
        {
            Aggregator = cons!.Resolve<IEventAggregator>();
            Aggregator!.Subscribe(this);
            base.OnInitialized();
        }
        private int GetCardIndex(T card) => DataContext!.CardList.IndexOf(card);
        protected override void OnParametersSet()
        {
            foreach (var card in DataContext!.CardList)
            {
                card.Location = GetStartingPoint(GetCardIndex(card));
            }
            base.OnParametersSet();
        }
        private readonly int _marginLeft = 10;
        private readonly int _labelHeight = 4;
        private readonly int _marginBottom = 2;
        private int TotalRows => DataContext!.ViewList.Max(x => x.Row);
        private int TotalColumns => DataContext!.ViewList.Max(x => x.Column);
        private string GetEntireCss
        {
            get
            {
                float heights = _labelHeight + CalculateHeight();
                if (TotalRows == 2)
                {
                    heights *= 2;
                }
                heights += _marginBottom * 3;
                if (TotalColumns == 2)
                {
                    float widths;
                    if (ExtraLongSecondColumn == false)
                    {
                        float firsts = CalculateWidth();
                        widths = firsts + _marginLeft + _marginLeft + 3;
                    }
                    else
                    {
                        widths = 70;
                    }
                    return $"height: {heights}vh; width: {widths}vw;"; //this is fine when its 2 columns.
                }
                return $"height: {heights}vh; width: 50vw;"; //try to make this 50 percent period.  hopefully i won't regret this.
            }
        }
        private string GetCardCss(T card)
        {
            return $"position: absolute; top: {card.Location.Y}vh; left: {card.Location.X}vw;";
        }
        private T GetCardInfo(TrickCoordinate trick)
        {
            int index = DataContext!.ViewList!.IndexOf(trick);
            return DataContext.CardList[index];
        }
        private int GetLeft(int column)
        {
            int lefts = _marginLeft + CalculateWidth();
            lefts *= (column - 1);
            return lefts;
        }
        private string GetLeft(TrickCoordinate trick)
        {
            if (trick.Column == 1)
            {
                return "Left: 0px;";
            }
            int lefts = GetLeft(trick.Column);
            return $"left: {lefts}vw;";
        }
        private int GetCardTop(int row)
        {
            int tops;
            tops = _labelHeight + _marginBottom;
            if (row == 1)
            {
                return tops;
            }
            int adds = TargetHeight;
            tops *= 2;
            tops += adds += _marginBottom;
            return tops;
        }
        private string GetLabelTop(TrickCoordinate trick)
        {
            if (trick.Row == 1)
            {
                return "top: 0px;";
            }
            int tops;
            tops = _labelHeight + _marginBottom;
            tops += TargetHeight;
            tops += _marginBottom;
            return $"top: {tops}vh;";
        }
        private string RealLabelHeight => $"font-size: {_labelHeight}vh;"; //can adjust as needed.
        private string GetLabelCss(TrickCoordinate trick)
        {
            string lefts = GetLeft(trick);

            string output = $"position: absolute; top: 0px; {lefts} {GetLabelTop(trick)} {RealLabelHeight}";
            return output;
        }
        private int CalculateWidth()
        {
            var firsts = TargetHeight * _tempSize.Width;
            return (int)firsts / (int)_tempSize.Height;
        }
        private int CalculateHeight()
        {
            var firsts = TargetHeight * _tempSize.Width;
            return (int)firsts / (int)_tempSize.Width;
        }
    }
}