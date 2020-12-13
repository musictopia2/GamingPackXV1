using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.Dominos;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace LottoDominosCP.Data
{
    [SingletonGame]
    public class LottoDominosVMData : ObservableObject, IViewModelData
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
        internal DominosBasicShuffler<SimpleDominoInfo> DominosList { get; set; }
        //looks like the shuffler has to be here.
        public LottoDominosVMData()
        {
            DominosList = new DominosBasicShuffler<SimpleDominoInfo>(); //hopefully does not have to register this time.
        }

    }
}