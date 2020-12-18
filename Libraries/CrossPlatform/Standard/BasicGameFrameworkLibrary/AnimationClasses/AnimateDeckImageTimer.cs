using BasicGameFrameworkLibrary.BasicGameDataClasses;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.AnimationClasses
{
    public class AnimateDeckImageTimer
    {
        public Action? StateChanged { get; set; }
        public int LongestTravelTime { get; set; }
        private const int _interval = 20;
        private double _startY;
        private int _totalSteps;
        private double _destinationY; //only y for this one.

        private readonly bool _fastAnimation;

        public AnimateDeckImageTimer()
        {
            BasicData basic = cons!.Resolve<BasicData>();
            _fastAnimation = basic.FastAnimation;
        }

        public double LocationYFrom { get; set; }
        public double LocationYTo { get; set; }
        public double CurrentYLocation { get; set; } = -1000; //if -1000, then location must be at 0 after all.
        public async Task DoAnimateAsync()
        {
            if (_fastAnimation)
            {
                CurrentYLocation = LocationYFrom;
                await FastSubmitAsync();
                _destinationY = LocationYTo;
                double totalYDistance;
                _startY = LocationYFrom;
                totalYDistance = _destinationY - _startY;
                var fins = totalYDistance / 2;
                CurrentYLocation += fins;
                await FastSubmitAsync();
                //CurrentYLocation = LocationYTo;
                FinishAnimation();
                return;
            }

            await Task.Run(() =>
            {
                CurrentYLocation = LocationYFrom;
                _startY = LocationYFrom;
                _destinationY = LocationYTo;
                var temps = LongestTravelTime / _interval;
                _totalSteps = temps;
                RunAnimationsAsync();
            });
        }

        private async Task FastSubmitAsync()
        {
            StateChanged!.Invoke();
            await Task.Delay(5);
        }

        private double _eachy;
        private double _upToy;
        private async void RunAnimationsAsync() //attempt void here.
        {
            double totalYDistance;
            totalYDistance = _destinationY - _startY;
            _eachy = totalYDistance / _totalSteps;
            _upToy = _startY;
            for (int i = 0; i < _totalSteps; i++)
            {
                _upToy += _eachy;
                CurrentYLocation = _upToy;
                StateChanged!.Invoke();
                await Task.Delay(_interval);
            }
            CurrentYLocation = _destinationY + 100;
            FinishAnimation();
        }

        private void FinishAnimation()
        {
            CurrentYLocation = -1000;
            StateChanged!.Invoke();
            BasicEventModels.EventExtensions.AnimationCompleted = true;
        }

    }
}