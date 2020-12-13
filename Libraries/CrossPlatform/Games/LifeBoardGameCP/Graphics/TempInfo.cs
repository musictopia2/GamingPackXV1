using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using LifeBoardGameCP.Data;
namespace LifeBoardGameCP.Graphics
{
    public class TempInfo : ObservableObject
    {
        public EnumViewCategory CurrentView { get; set; }
        public int HeightWidth { get; set; }
        private int _currentNumber;
        public int CurrentNumber
        {
            get
            {
                return _currentNumber;
            }
            set
            {
                if (SetProperty(ref _currentNumber, value) == true)
                {
                }
            }
        }
        public CustomBasicList<PositionInfo> PositionList { get; set; } = new CustomBasicList<PositionInfo>(); //decided to try custom one here.  hopefully the risk pays off. otherwise, has to put old fashioned list back.
    }
}