using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class RegularLowAceCalculator : IRegularAceCalculator
    {
        void IRegularAceCalculator.PopulateAceValues(IRegularCard thisCard)
        {
            thisCard.Value = EnumRegularCardValueList.LowAce;
        }
    }
    public class RegularAceHighCalculator : IRegularAceCalculator
    {
        void IRegularAceCalculator.PopulateAceValues(IRegularCard thisCard)
        {
            thisCard.Value = EnumRegularCardValueList.HighAce;
        }
    }
    public class RegularAceHighSimpleDeck : IRegularDeckInfo
    {
        public int HowManyDecks => 1;
        public bool UseJokers => false;
        public int GetExtraJokers => 0;
        public int LowestNumber => 2; //ordinary, ace will be low.  in cases wheer
        public int HighestNumber => 14;
        public CustomBasicList<ExcludeRCard> ExcludeList => new CustomBasicList<ExcludeRCard>();
        public CustomBasicList<EnumSuitList> SuitList => Helpers.GetCompleteSuitList;
        public int GetDeckCount()
        {
            return 52; //this has 52 cards in a standard deck.
        }
    }
    public class RegularAceLowSimpleDeck : IRegularDeckInfo
    {
        public int HowManyDecks => 1;
        public bool UseJokers => false;
        public int GetExtraJokers => 0;
        public int LowestNumber => 1; //ordinary, ace will be low.  in cases wheer
        public int HighestNumber => 13;
        public CustomBasicList<ExcludeRCard> ExcludeList => new CustomBasicList<ExcludeRCard>();
        public CustomBasicList<EnumSuitList> SuitList => Helpers.GetCompleteSuitList;
        public int GetDeckCount()
        {
            return 52; //this has 52 cards in a standard deck.
        }
    }
}