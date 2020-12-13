using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using ThreeLetterFunCP.Logic;
namespace ThreeLetterFunCP.Data
{
    [SingletonGame]
    public class ThreeLetterFunVMData : ObservableObject, IViewModelData
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
        [VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
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

        public INewCard? NewUI;

        public TileBoardObservable? TileBoard1 { get; set; }



        private string _playerWon = "";
        [VM]
        public string PlayerWon
        {
            get { return _playerWon; }
            set
            {
                if (SetProperty(ref _playerWon, value))
                {

                }
            }
        }
        private ThreeLetterFunCardData? _currentCard;
        [VM]
        public ThreeLetterFunCardData? CurrentCard
        {
            get { return _currentCard; }
            set
            {
                if (SetProperty(ref _currentCard, value))
                {

                }
            }
        }
    }
}