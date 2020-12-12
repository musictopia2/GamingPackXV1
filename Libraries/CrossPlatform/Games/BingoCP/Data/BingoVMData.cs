using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BingoCP.Data
{
    [SingletonGame]
    public class BingoVMData : ObservableObject, IViewModelData
    {
        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value))
                {
                    
                }
            }
        }
        private string _status = "";
        [VM]
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value))
                {
                    
                }
            }
        }
        private string _numberCalled = "";
        [VM]
        public string NumberCalled
        {
            get { return _numberCalled; }
            set
            {
                if (SetProperty(ref _numberCalled, value))
                {
                    
                }
            }
        }
    }
}