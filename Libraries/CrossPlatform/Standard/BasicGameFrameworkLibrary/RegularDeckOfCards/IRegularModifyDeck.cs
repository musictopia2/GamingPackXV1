namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public interface IRegularModifyDeck //i think this should be separate.
    {
        void RemoveSuit(EnumSuitList suitToRemove);
        void ReloadSuits();
    }
}