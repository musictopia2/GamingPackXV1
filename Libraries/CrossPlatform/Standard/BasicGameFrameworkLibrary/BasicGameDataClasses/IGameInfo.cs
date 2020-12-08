namespace BasicGameFrameworkLibrary.BasicGameDataClasses
{
    public interface IGameInfo
    {
        EnumGameType GameType { get; } //no more support for whether its solitaire or not for cards.  does not belong here for sure.
        bool CanHaveExtraComputerPlayers { get; }
        EnumPlayerChoices SinglePlayerChoice { get; }
        EnumPlayerType PlayerType { get; }
        string GameName { get; }
        int NoPlayers { get; } //to support games like milebornes where 5 players are not allowed.
        int MinPlayers { get; }
        int MaxPlayers { get; }
        bool CanAutoSave { get; }
        EnumSmallestSuggested SmallestSuggestedSize { get; } //decided to have just as suggestion.  however, something else will decide what to do.
        EnumSuggestedOrientation SuggestedOrientation { get; } //decided to have each game decide the suggested orientation.
    }
}