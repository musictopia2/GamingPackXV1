namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerItem
    {
        int Id { get; set; }
        string NickName { get; set; } //i do like this part.  even if its human 1 and human 2.
        bool InGame { get; set; }
        bool IsReady { get; set; }
        bool MissNextTurn { get; set; } //sometimes there is a game where they have to miss next turn.
        bool IsHost { get; set; } //unfortunately this is needed too.
        bool CanStartInGame { get; } //usually will be true but can vary.
        EnumPlayerCategory PlayerCategory { get; set; }
    }
}