namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerNeeds
    {
        int PlayersNeeded { get; } //games like clue will fill this out.  so when doing the players, can have extra idle players.
    }
}