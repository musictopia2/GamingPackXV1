using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Drawing;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.AnimationClasses
{
    public class AnimateTrickClass<S, T>
        where S : struct, Enum
        where T : class, ITrickCard<S>, new()
    {
        private class TrickInfo
        {
            public float StartX { get; set; }
            public float StartY { get; set; }
            public float DiffX { get; set; }
            public float DiffY { get; set; }
            public float CurrentX { get; set; }
            public float CurrenyY { get; set; }
            public int Index { get; set; } // needs index because the winning card will not be here.
        }
        public int LongestTravelTime { get; set; }
        private const float _internval = 20;
        private int _destinationX;
        private int _destinationY;
        private int _totalSteps;
        private CustomBasicList<TrickInfo>? _trickList;
        private BasicTrickAreaObservable<S, T>? _tempArea;
        public Action? StateChanged { get; set; }
        public Func<int, PointF>? GetStartingPoint { get; set; }

        //private bool _xup = false;
        //private bool _yup = false;


        public async Task MovePiecesAsync(BasicTrickAreaObservable<S, T> thisArea)
        {
            if (GetStartingPoint == null || StateChanged == null)
            {
                return; //i think.
            }
            _tempArea = thisArea;
            _destinationX = (int)thisArea.WinCard!.Location.X;
            _destinationY = (int)thisArea.WinCard.Location.Y;
            var index = thisArea.CardList.IndexOf(thisArea.WinCard);
            var thisLoc = GetStartingPoint(index);
            _destinationX = (int)thisLoc.X;
            _destinationY = (int)thisLoc.Y;
            _trickList = new CustomBasicList<TrickInfo>();
            var temps = LongestTravelTime / _internval;
            _totalSteps = (int)temps;
            //maybe does not need the part for iswasm anymore since maybe animations will be better now with all my changes.
            //if (BasicData.IsWasm)
            //{
            //    _totalSteps = 3; //try this way to emulate being faster for web assembly.
            //}
            foreach (var thisCard in _tempArea.CardList)
            {
                if (thisCard.Equals(thisArea.WinCard) == false)
                {
                    TrickInfo thisTrick = new TrickInfo();
                    thisTrick.Index = _tempArea.CardList.IndexOf(thisCard);
                    var thisLocation = GetStartingPoint(thisTrick.Index);
                    thisTrick.CurrentX = thisLocation.X;
                    thisTrick.CurrenyY = thisLocation.Y;
                    thisTrick.StartX = thisLocation.X;
                    thisTrick.StartY = thisLocation.Y;
                    double totalxDistance;
                    double totalYDistance;
                    totalxDistance = (double)_destinationX - thisTrick.StartX;
                    totalYDistance = (double)_destinationY - thisTrick.StartY;
                    thisTrick.DiffX = (float)totalxDistance / _totalSteps;
                    thisTrick.DiffY = (float)totalYDistance / _totalSteps;
                    _trickList.Add(thisTrick);
                }
            }
            await RunAnimationsAsync();
        }
        private async Task RunAnimationsAsync()
        {
            if (StateChanged == null || _tempArea == null)
            {
                return;
            }
            await Task.Delay(500); // so others can see what was put down before the animations start.
            int x;

            var loopTo = _totalSteps - 1;
            //taking a risk.   may add one for all cases now (?)

            //if (BasicData.IsWasm)
            //{
            //    loopTo++;
            //}
            for (x = 1; x <= loopTo; x++)
            {
                await Task.Delay((int)_internval);
                foreach (var thisTrick in _trickList!)
                {
                    thisTrick.CurrentX += thisTrick.DiffX;
                    thisTrick.CurrenyY += thisTrick.DiffY;
                    var card = _tempArea.CardList[thisTrick.Index];
                    card.Location = new PointF(thisTrick.CurrentX, thisTrick.CurrenyY);
                }
                StateChanged(); //try this way now (?)
            }
            foreach (var thisTrick in _trickList!)
            {
                var card = _tempArea.CardList[thisTrick.Index];
                card.Location = new PointF(thisTrick.StartX, thisTrick.StartY);
            }
            //if (BasicData.IsWasm == false)
            //{
            //    StateChanged();
            //}
        }
    }
}