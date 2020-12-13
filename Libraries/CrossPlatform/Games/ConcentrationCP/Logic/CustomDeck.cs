using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace ConcentrationCP.Logic
{
    public class CustomDeck : IRegularDeckInfo
    {
        public int HowManyDecks => 1;
        public bool UseJokers => false;
        public int GetExtraJokers => 0;
        public int LowestNumber => 1;
        public int HighestNumber => 13;
        CustomBasicList<ExcludeRCard> IRegularDeckInfo.ExcludeList
        {
            get
            {
                CustomBasicList<ExcludeRCard> output = new CustomBasicList<ExcludeRCard>();
                output.AppendExclude(EnumSuitList.Clubs, 1)
                    .AppendExclude(EnumSuitList.Spades, 1);
                return output;
            }
        }
        public CustomBasicList<EnumSuitList> SuitList => Helpers.GetCompleteSuitList;
        public int GetDeckCount()
        {
            return 50;
        }
    }
}