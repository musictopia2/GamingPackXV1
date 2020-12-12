using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BingoCP.Data
{
    public class BingoItem : ObservableObject, IBasicSpace
    {
        public Vector Vector { get; set; }

        private int _whatValue;
        public int WhatValue
        {
            get { return _whatValue; }
            set
            {
                if (SetProperty(ref _whatValue, value))
                {
                    
                }
            }
        }
        private string _letter = "";
        public string Letter
        {
            get { return _letter; }
            set
            {
                if (SetProperty(ref _letter, value))
                {
                    
                }

            }
        }
        private bool _didGet;

        public bool DidGet
        {
            get { return _didGet; }
            set
            {
                if (SetProperty(ref _didGet, value))
                {
                    
                }
            }
        }
        public void ClearSpace()
        {
            DidGet = false;
        }
        public bool IsFilled()
        {
            return DidGet;
        }
    }
}