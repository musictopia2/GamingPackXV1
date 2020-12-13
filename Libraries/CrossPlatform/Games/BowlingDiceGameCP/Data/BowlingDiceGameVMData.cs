using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BowlingDiceGameCP.Data
{
    [SingletonGame]
    public class BowlingDiceGameVMData : ObservableObject, IViewModelData
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
        private bool _isExtended;
        [VM]
        public bool IsExtended
        {
            get { return _isExtended; }
            set
            {
                if (SetProperty(ref _isExtended, value))
                {
                    
                }
            }
        }
        private int _whichPart;
        [VM]
        public int WhichPart
        {
            get { return _whichPart; }
            set
            {
                if (SetProperty(ref _whichPart, value))
                {
                    
                }
            }
        }
        private int _whatFrame;
        [VM]
        public int WhatFrame
        {
            get { return _whatFrame; }
            set
            {
                if (SetProperty(ref _whatFrame, value))
                {
                    
                }
            }
        }
    }
}