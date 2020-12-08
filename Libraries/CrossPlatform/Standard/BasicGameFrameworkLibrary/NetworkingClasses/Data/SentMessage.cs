using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Newtonsoft.Json;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Data
{
    public class SentMessage : ObservableObject
    {
        private string _status = "";
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value)) { }
            }
        }
        private string _body = "";
        public string Body
        {
            get { return _body; }
            set
            {
                if (SetProperty(ref _body, value)) { }
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented); //i do like indented.   has to be this way because can't use async in this situation
        }
    }
}