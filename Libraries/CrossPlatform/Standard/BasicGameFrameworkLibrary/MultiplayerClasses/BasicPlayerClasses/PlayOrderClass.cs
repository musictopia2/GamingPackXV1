namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public class PlayOrderClass : IPlayOrder
    {
        public int WhoTurn { get; set; }
        public int OtherTurn { get; set; }
        public bool IsReversed { get; set; }
        public int WhoStarts { get; set; }
    }
}