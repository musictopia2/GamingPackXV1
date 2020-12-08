namespace BasicGameFrameworkLibrary.NetworkingClasses.Data
{
    public class NetworkMessage
    {
        public string YourNickName { get; set; } = ""; // who did it come from.
        public string SpecificPlayer { get; set; } = ""; //defaults to blank.
        public string Message { get; set; } = "";
        public EnumNetworkCategory NetworkCategory { get; set; }
    }
}