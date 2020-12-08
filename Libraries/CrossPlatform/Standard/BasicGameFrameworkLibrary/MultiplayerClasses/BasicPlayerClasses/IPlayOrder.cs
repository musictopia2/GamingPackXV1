namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayOrder //you can swap out how you decide the play order.  just something needs to know this.
    {
        int WhoTurn { get; set; }
        int OtherTurn { get; set; }
        bool IsReversed { get; set; }
        int WhoStarts { get; set; }
    }
}