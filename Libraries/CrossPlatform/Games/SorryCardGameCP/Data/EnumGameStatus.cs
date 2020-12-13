namespace SorryCardGameCP.Data
{
    public enum EnumGameStatus
    {
        Regular = 0,
        ChoosePlayerToSorry = 1, // this is when playing a regular sorry card
        WaitForSorry21 = 2,
        HasDontBeSorry = 3
    }
}