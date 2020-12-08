using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public abstract class SimpleSave : ObservableObject
    {
        public bool CanPrivateSave { get; set; }

        public string GameID { get; set; } = "";
        public void GetNewID()
        {
            GameID = Guid.NewGuid().ToString();
        }
    }
}