using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Collections.Generic;
namespace MancalaCP.Data
{
    [SingletonGame]
    public class MancalaVMData : ObservableObject, IViewModelData
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
        public int SpaceSelected { get; set; }
        public int SpaceStarted { get; set; }
        internal Dictionary<int, SpaceInfo> SpaceList { get; set; } = new Dictionary<int, SpaceInfo>(); //this should be the main list.
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
        private int _piecesAtStart;
        [VM]
        public int PiecesAtStart
        {
            get { return _piecesAtStart; }
            set
            {
                if (SetProperty(ref _piecesAtStart, value))
                {
                    
                }
            }
        }
        private int _piecesLeft;
        [VM]
        public int PiecesLeft
        {
            get { return _piecesLeft; }
            set
            {
                if (SetProperty(ref _piecesLeft, value))
                {
                    
                }
            }
        }
    }
}