using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using XPuzzleCP.Logic;
namespace XPuzzleCP.Data
{
    [SingletonGame]
    public class XPuzzleSaveInfo : ObservableObject, IMappable
    {
        private Vector _previousOpen; //has to make it a vector now.

        public Vector PreviousOpen
        {
            get { return _previousOpen; }
            set
            {
                if (SetProperty(ref _previousOpen, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        public XPuzzleCollection SpaceList = new XPuzzleCollection();
    }
}