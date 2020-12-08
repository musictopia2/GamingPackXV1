namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public interface IRegularAceCalculator //i think the individaul should do this, not the deck part.
    {
        void PopulateAceValues(IRegularCard thisCard); //this is all this does.
    }
}