namespace FluxxCP.Data
{
    public enum EnumEndTurnStatus
    {
        Successful = 0, // this means it can end turn
        Hand = 1,
        Play = 2,
        Keeper = 3,
        Goal = 4
    }
}