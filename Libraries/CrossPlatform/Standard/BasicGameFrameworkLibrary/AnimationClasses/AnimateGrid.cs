using System;
using System.Drawing;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.AnimationClasses
{
    public class AnimateGrid
    {
        public Action? StateChanged { get; set; }
        public int LongestTravelTime { get; set; }
        private const float _interval = 20;
        private int _destinationX;
        private int _destinationY;
        private int _startX;
        private int _startY;
        private int _totalSteps;
        public PointF LocationFrom { get; set; }
        public PointF LocationTo { get; set; } //no skiasharp anymore
        public PointF CurrentLocation { get; set; } = new PointF(-1000, -1000);
        public async Task DoAnimateAsync()
        {
            CurrentLocation = new PointF(LocationFrom.X, LocationFrom.Y);
            _startX = (int)LocationFrom.X;  // myLocation.X
            _startY = (int)LocationFrom.Y; // myLocation.Y
            _destinationX = (int)LocationTo.X;
            _destinationY = (int)LocationTo.Y;
            var temps = LongestTravelTime / _interval;
            _totalSteps = (int)temps;
            await RunAnimationsAsync();
        }

        private async Task RunAnimationsAsync()
        {
            //this is like the other but it has x and y.
            double totalXDistance;
            double totalYDistance;
            double eachx = 0;
            double eachy = 0;
            int x;
            int upTox = 0;
            int upToy = 0;
            await Task.Run(() =>
            {
                totalXDistance = _destinationX - _startX;
                totalYDistance = _destinationY - _startY;
                eachx = totalXDistance / _totalSteps;
                eachy = totalYDistance / _totalSteps;
                upTox = _startX;
                upToy = _startY;
            });
            var loopTo = _totalSteps - 1;
            for (x = 1; x <= loopTo; x++)
            {
                await Task.Delay((int)_interval);
                upTox += (int)eachx;
                upToy += (int)eachy;
                CurrentLocation = new PointF(upTox, upToy);
                StateChanged!.Invoke();
            }
            CurrentLocation = new PointF(_destinationX, _destinationY);
            StateChanged!.Invoke();
            await Task.Delay(50);
            CurrentLocation = new PointF(-1000, -1000);
        }
    }
}