namespace CribbageCP.Data
{
    public enum EnumGameStatus
    {
        None = 0,
        CardsForCrib = 1,
        PlayCard = 2, // only for when playing card that it will save the info
        GetResultsHand = 3,
        GetResultsCrib = 4
    }
}