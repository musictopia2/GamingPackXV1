using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
using System.Drawing;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.AnimationClasses
{
    public class AnimateBasicGameBoard
    {
        readonly EventAggregator _thisE;
        public PointF LocationFrom { get; set; }
        public PointF LocationTo { get; set; }
        public PointF CurrentLocation { get; set; }

        public bool FastAnimation { get; set; } //mexican train dominos will use the option.  the game has to decide whether to enable this.

        public bool AnimationGoing { get; set; } // so the gameboard knows whether it needs something special or not.
        private float _destinationX;
        private float _destinationY;
        private float _startX;
        private float _startY;
        private int _totalSteps;
        public int LongestTravelTime { get; set; }
        private const int _interval = 20;
        public AnimateBasicGameBoard()
        {
            _thisE = Resolve<EventAggregator>();
        }

        private bool _xup = false;
        private bool _yup = false;

        public async Task DoAnimateAsync()
        {
            _startX = LocationFrom.X;  // myLocation.X
            _startY = LocationFrom.Y; // myLocation.Y
            _destinationX = LocationTo.X;
            _destinationY = LocationTo.Y;
            _totalSteps = LongestTravelTime / _interval;
            CurrentLocation = LocationFrom; // for now.
            _xup = LocationTo.X > LocationFrom.X;
            _yup = LocationTo.Y > LocationFrom.Y;
            AnimationGoing = true; // so when they access the information, they will do something different.
            _thisE.RepaintMessage(EnumRepaintCategories.Main); //try main this time





            float totalXDistance;
            float totalYDistance;
            float eachx = 0;
            float eachy = 0;
            int x;
            float upTox = 0;
            float upToy = 0;


            //attempt to not even run the steps part (since i found ways to improve performance now


            //if (BasicData.IsWasm)
            //{
            //    _totalSteps = 3; //try 3 steps for web assembly.  that is the next thing to try.
            //}
            if (FastAnimation)
            {
                _totalSteps = 2;
            }
            await Task.Run(() =>
            {
                totalXDistance = _destinationX - _startX;
                totalYDistance = _destinationY - _startY;
                eachx = totalXDistance / _totalSteps;
                eachy = totalYDistance / _totalSteps;
                upTox = _startX;
                upToy = _startY;
            });
            int loopTo = _totalSteps;
            if (FastAnimation)
            {
                loopTo = 1;
            }
            for (x = 1; x <= loopTo; x++)
            {
                upTox += eachx;
                upToy += eachy;
                if (_xup && upTox > LocationTo.X)
                {
                    upTox = LocationTo.X;
                }
                else if (_xup == false && upTox < LocationTo.X)
                {
                    upTox = LocationTo.X;
                }
                if (_yup && upToy > LocationTo.Y)
                {
                    upToy = LocationTo.Y;
                }
                else if (_yup == false && upToy < LocationTo.Y)
                {
                    upToy = LocationTo.Y;
                }
                CurrentLocation = new PointF(upTox, upToy);
                _thisE.RepaintMessage(EnumRepaintCategories.Main);
                if (FastAnimation)
                {
                    await Task.Delay(5);
                }
                else
                {
                    await Task.Delay(_interval);
                }
                //await Task.Delay(10);

                //no matter what, did not refresh properly.

                //await Task.Delay(1000); //for now.

            }
            if (FastAnimation)
            {
                CurrentLocation = LocationTo;
                _thisE.RepaintMessage(EnumRepaintCategories.Main);
                await Task.Delay(5);
            }
            AnimationGoing = false;
            RepaintEventModel.UpdatePartOfBoard = null; // no matter what.
        }
    }
}