namespace BasicGameFrameworkLibrary.BasicEventModels
{
    public class SocketErrorEventModel
    {
        public EnumSocketCategory Category { get; set; }
        public string Message { get; set; } = "";
    }
}