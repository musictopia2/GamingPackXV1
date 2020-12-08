using BasicGameFrameworkLibrary.BasicGameDataClasses;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
namespace BasicGameFrameworkLibrary.AnimationClasses
{
    public class AnimateDeckImageTimer : IDisposable
    {
        //looks like i have to do as timer for the cards.  not sure why others worked but not this one.  does not matter why.
        public Action? StateChanged { get; set; }
        public int LongestTravelTime { get; set; }
        private const float _interval = 20;
        private double _startY;
        private int _totalSteps;
        private double _destinationY; //only y for this one.
        public double LocationYFrom { get; set; }
        public double LocationYTo { get; set; }
        public double CurrentYLocation { get; set; } = -1000; //if -1000, then location must be at 0 after all.
        public async Task DoAnimateAsync()
        {
            //if (BasicData.IsWasm)
            //{
            //    CurrentYLocation = -1000;
            //    BasicEventModels.EventExtensions.AnimationCompleted = true;
            //    return; //try to ignore the animations since there seems to be no way currently to speed it up.
            //}
            CurrentYLocation = LocationYFrom;
            _startY = LocationYFrom;
            _destinationY = LocationYTo;
            var temps = LongestTravelTime / _interval;
            _totalSteps = (int)temps;
            //if (BasicData.IsWasm)
            //{
            //    _totalSteps = 6; //use same idea as i did for the basicgameboard.
            //}
            await RunAnimationsAsync();
        }
        public AnimateDeckImageTimer()
        {
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            Timer.Interval = _interval;
            Timer.AutoReset = false;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();

            //has to figure out how to make it show the one that should have been there afterwards.

            

            _upToy += _eachy;
            _loopup++;
            if (_loopup >= _totalSteps - 1)
            {
                CurrentYLocation = _destinationY + 100;

                //not sure if we need that part (?)

                //StateChanged!.Invoke();
                //System.Threading.Thread.Sleep(1000);
                //System.Threading.Thread.Sleep(10); //hopefully this works now.
                //if (BasicData.IsWasm == false)
                //{
                //    System.Threading.Thread.Sleep(50); //show reaching destination.
                //}
                CurrentYLocation = -1000;
                StateChanged!.Invoke();
                BasicEventModels.EventExtensions.AnimationCompleted = true; 
                return;
            }
            CurrentYLocation = _upToy;
            StateChanged!.Invoke();
            Timer.Start();
        }
        private Timer Timer { get; set; }
        private double _eachy;
        private double _upToy;
        private int _loopup;
        private async Task RunAnimationsAsync() //unfortunately needs timer.
        {
            double totalYDistance;
            await Task.Run(() =>
            {
                totalYDistance = _destinationY - _startY;
                _eachy = totalYDistance / _totalSteps;
                _upToy = _startY;
            });
            _loopup = 1;
            Timer.Start();
        }
        public void Dispose()
        {
            Timer.Elapsed -= Timer_Elapsed;
            Timer.Dispose();
        }
    }
}