using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace PokerCP.Data
{
    public class DisplayCard : ObservableObject //for now 2 classes.  may be able to do as one (?)
    {

        private PokerCardInfo? _currentCard;

        public PokerCardInfo CurrentCard
        {
            get { return _currentCard!; }
            set
            {
                if (SetProperty(ref _currentCard, value))
                { 
                }

            }
        }
        private bool _willHold;
        public bool WillHold
        {
            get
            {
                return _willHold;
            }

            set
            {
                if (SetProperty(ref _willHold, value) == true)
                {
                    OnPropertyChanged(nameof(Text));
                }
            }
        }


        public string Text
        {
            get
            {
                if (WillHold == true)
                {
                    return "Hold";
                }
                return "";
            }
        }


    }
}
