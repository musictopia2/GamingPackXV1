using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BingoCP.Data
{
    public class SpaceInfoCP : ObservableObject, IBasicSpace
    {
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (SetProperty(ref _isEnabled, value))
                {
                    
                }
            }
        }
        public Vector Vector { get; set; }
        private bool _alreadyMarked;
        public bool AlreadyMarked
        {
            get { return _alreadyMarked; }
            set
            {
                if (SetProperty(ref _alreadyMarked, value))
                {
                    
                }
            }
        }
        private string _text = "";
        public string Text
        {
            get { return _text; }
            set
            {
                if (SetProperty(ref _text, value))
                {
                    
                }
            }
        }
        public void ClearSpace()
        {
            AlreadyMarked = false;
        }
        public bool IsFilled()
        {
            return AlreadyMarked;
        }
    }
}