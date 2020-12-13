namespace BattleshipCP.Data
{
    public enum EnumStatusList
    {
        None = 0, // because at the very beginning, can't do anything.
        PlacingShips = 1, // this means the players are placing ships
        InGame = 2 // this means its in the game
    }
}