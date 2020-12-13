using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace SorryCP.Data
{
    [SingletonGame]
    public class SorryVMData : ObservableObject, ISimpleBoardGamesData
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
        private string _instructions = "";
        [VM]
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    
                }
            }
        }
        private string _cardDetails = "";
        [VM]
        public string CardDetails
        {
            get { return _cardDetails; }
            set
            {
                if (SetProperty(ref _cardDetails, value))
                {
                    
                }
            }
        }
    }
}