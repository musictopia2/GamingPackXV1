using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Data
{
    public class PlayerInfo : ObservableObject
    {
        private string _nickName = "";
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                if (SetProperty(ref _nickName, value) == true) { }
            }
        }
        private bool _isHost;
        public bool IsHost
        {
            get
            {
                return _isHost;
            }

            set
            {
                if (SetProperty(ref _isHost, value) == true) { }
            }
        }
    }
}