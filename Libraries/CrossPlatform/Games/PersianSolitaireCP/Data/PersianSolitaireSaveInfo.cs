using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace PersianSolitaireCP.Data
{
    [SingletonGame]
    public class PersianSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        private int _dealNumber;
        public int DealNumber
        {
            get { return _dealNumber; }
            set
            {
                if (SetProperty(ref _dealNumber, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
    }
}