using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Data
{
    public class FirstGameData : ObservableObject// this is  json now.
    {
        private bool _client;
        public bool Client
        {
            get
            {
                return _client;
            }
            set
            {
                if (SetProperty(ref _client, value) == true) { }
            }
        }
        public CustomBasicCollection<PlayerInfo> PlayerList { get; set; } = new CustomBasicCollection<PlayerInfo>(); //i think
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
    }
}