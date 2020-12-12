using CommonBasicStandardLibraries.Exceptions;
using Newtonsoft.Json;
namespace BasicGameFrameworkLibrary.StandardImplementations.Settings
{
    //eventually will add support for desktop/mobile native in .net 6.
    public class GlobalDataModel
    {
        public string NickName { get; set; } = ""; //decided class because it does need to change this time.
        public string CustomAzureEndPoint { get; set; } = "";
        public EnumAzureMode ServerMode = EnumAzureMode.Public; //default to public.
        private const string _defaultPrivateEndPoint = "https://onlinegameserver.azurewebsites.net/"; //has to keep 
        private const string _defaultPublicEndPoint = "https://gamingpackxpublicserver.azurewebsites.net";
        public string GetEndPoint()
        {
            if (ServerMode == EnumAzureMode.Custom && CustomAzureEndPoint == "")
            {
                //if a person enters wrong address, they have to refresh and choose other options.
                return _defaultPublicEndPoint; //default to public if choosing custom but does not exist.
            }
            return ServerMode switch
            {
                EnumAzureMode.Custom => CustomAzureEndPoint,
                EnumAzureMode.Private => _defaultPrivateEndPoint,
                _ => _defaultPublicEndPoint,
            };
        }
        public static string LocalStorageKey => "settingsv1";
        [JsonIgnore] //just to make sure this gets ignored.
        public static GlobalDataModel? DataContext { get; set; }

        public static bool NickNameAcceptable()
        {
            if (DataContext == null)
            {
                throw new BasicBlankException("Settings data failed to load.");
            }
            return string.IsNullOrWhiteSpace(DataContext.NickName) == false;
        }
    }
}